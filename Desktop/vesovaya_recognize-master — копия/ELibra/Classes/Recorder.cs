using Accord.Video;
using Accord.Video.FFMPEG;
using ELibra.DBModel;
using ELibra.DBModel.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ELibra.Classes
{
    public class Recorder
    {
        #region Переменные для работы с видео
        private VideoFileWriter fileWriter = new VideoFileWriter();
        private Bitmap frame;
        private VideoFileSource source;
        private AsyncVideoSource aSource;
        private double weight = 0;
        private double maxWeight = 0;
        private double startWeight = 0;
        private double finishWeight = 0;
        private DateTime startDate;
        private DateTime endDate;
        private string fileName;
        private string url;
        private string scale;
        private int num;
        private bool fact = false;
        private int h;
        private int w;
        private long countFrames;
        private string path;
        public bool isRecord = false;
        private bool isStoped = false;
        private VideoSource videoSource;
        #endregion

        public double Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public Recorder(string url, int num, bool fact, ref VideoSource source, string scale)
        {
            this.url = url;
            this.num = num;
            this.fact = num == 0;
            this.scale = scale;

            if (fact)
            {
                using (DataBaseContext context = new DataBaseContext())
                {
                    DateTime dateNow = DateTime.Now;
                    dateNow = dateNow.AddMilliseconds(-dateNow.Millisecond);

                    //var user = (from i in context.Settings
                    //            select i.fioOperator).FirstOrDefault();
                    //int userId = (from i in context.Users
                    //              where i.FIO == user
                    //              select i.id).FirstOrDefault();
                    Weighings w = new Weighings
                    {
                        //userId = userId,
                        dateWeight = dateNow,
                        updated = dateNow,
                        userId = 1,
                        taraScale = scale
                    };
                    context.Weighings.Add(w);
                    context.SaveChanges();
                }
            }

            #region Настройка размера кадра в зависимости от потока камеры
            if (url[url.IndexOf("stream=") + 7] == '0')
            {
                h = 1440;
                w = 2560;
            }
            else
            {
                h = 576;
                w = 704;
            }
            #endregion
            videoSource = source;
            new Thread(new ThreadStart(StartRecord)).Start();
        }

        private void StartRecord()
        {
            isRecord = true;
            #region Создание видеофайла
            using (DataBaseContext context = new DataBaseContext())
            {
                path = (from i in context.Settings
                       where i.id == 1
                       select i.PathVideo).FirstOrDefault();
                if (string.IsNullOrEmpty(path))
                {
                    path = "C:\\Video";
                }
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                fileName = path + "\\" + DateTime.Now.ToString("HH-mm-ss") + " " + num + " " + url[url.IndexOf("rtsp://=") + 18] + (url[url.IndexOf("rtsp://=") + 19].ToString() != "/" ? url[url.IndexOf("rtsp://=") + 19].ToString() : "") + ".mkv";
                Console.WriteLine(fileName);
                try
                {
                    fileWriter.Open(fileName, w, h, 15, VideoCodec.MPEG4);
                    videoSource.aSource.NewFrame += Source_NewFrame;
                    recordLiveCamStart();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            #endregion
        }

        private void Source_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (isRecord)
            {
                #region Отрисовка веса на кадре
                try
                {
                    countFrames += videoSource.aSource.FramesProcessed;
                    using (frame = (Bitmap)eventArgs.Frame.Clone())
                    {
                        using (var gr = Graphics.FromImage(frame))
                        {
                            gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            gr.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                            gr.DrawString(weight.ToString(), new Font("Arial", 24), Brushes.White, new RectangleF(10, 10, 200, 50));
                            gr.DrawString(scale, new Font("Arial", 24), Brushes.White, new RectangleF(10, 40, 1000, 50));
                        }
                        fileWriter.WriteVideoFrame(frame);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
               
                if (weight > maxWeight)
                    maxWeight = weight;
                GC.Collect();
                #endregion                
            }
            else
            {
                AbortRecord();
            }
        }

        public void AbortRecord()
        {
            try
            {
                if (!isStoped)
                {
                    #region Завершение записи видео в файл
                    isStoped = true;
                    fileWriter.Flush();
                    fileWriter.Close();
                    recordLiveCamStop();
                    videoSource.aSource.NewFrame -= Source_NewFrame;
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка в записи файла\n" + ex.Message + "\nМетод создавший ошибку " + ex.TargetSite + "\nПредставление кадров " + ex.StackTrace + "\nЭкземпляр класса " + ex.InnerException + "\nДоп. сведения " + ex.Data);
                //throw;
            }
        }

        private void recordLiveCamStop()
        {
            using (DataBaseContext context = new DataBaseContext())
            {
                #region Создание факта о взвешивании если это первая из запущенных камер
                if (countFrames > 0)
                {
                    finishWeight = weight;
                    endDate = DateTime.Now;
                    
                    if (fact)
                    {
                        var a = (from i in context.AutoWeighingFacts
                                 where i.startDate == startDate
                                 select i).FirstOrDefault();

                        a.finishDate = endDate;
                        a.finishWeight = finishWeight;
                        a.maxWeight = maxWeight;

                        context.SaveChanges();
                    }                
                }
                else
                {
                    #region Удаление ненужных строк если это первая из запущенных камер
                    if (fact)
                    {
                        var a = (from i in context.AutoWeighingFacts
                                 orderby i.id descending
                                 select i).FirstOrDefault();
                        var b = (from i in context.WeighingFactLinks
                                 orderby i.id descending
                                 select i).FirstOrDefault();
                        context.AutoWeighingFacts.Remove(a);
                        context.WeighingFactLinks.Remove(b);
                        context.SaveChanges();
                    }
                    #endregion
                }
                #endregion
            }
        }

        private void recordLiveCamStart()   
        {
            startDate = DateTime.Now;
            startWeight = weight;
            using (DataBaseContext context = new DataBaseContext())
            {
                #region Создание факта о взвешивании если это первая из запущенных камер
                if (fact)
                {
                    AutoWeighingFacts aF = new AutoWeighingFacts
                    {
                        startDate = startDate,
                        finishDate = endDate,
                        startWeight = startWeight,
                        finishWeight = finishWeight,
                        maxWeight = maxWeight
                    };
                    context.AutoWeighingFacts.Add(aF);
                    context.SaveChanges();
                }
                #endregion

                Thread.Sleep(700);

                #region Создание строки с данными о хранении видео
                int factId = (from i in context.AutoWeighingFacts
                              orderby i.id descending
                              select i.id).FirstOrDefault();
                AutoWeighingMovies aM = new AutoWeighingMovies
                {
                    factId = factId,
                    movieName = fileName
                };
                context.AutoWeighingMovies.Add(aM);
                #endregion

                #region Создание факта о взвешивании если это первая из запущенных камер
                if (fact)
                {
                    var wID = (from i in context.Weighings
                               orderby i.updated descending
                               select new { i.id, i.dateWeight }).FirstOrDefault();
                    WeighingFactLinks wL = new WeighingFactLinks
                    {
                        weighingId = wID.id,
                        factId = factId,
                        weightKind = 0,
                        dateWeight = wID.dateWeight
                    };
                    context.WeighingFactLinks.Add(wL);
                }
                #endregion

                context.SaveChanges();
            }
        }
    }
}
