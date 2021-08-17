using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vosk;

namespace VoskTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string path_eng = "E:\\Artem\\Vosk\\Wave\\red_five.wav";
            string model_eng = "E:\\Artem\\Vosk\\Models\\vosk-model-en-us-0.20";

            Vosk.Model model = new Model(model_eng);
            DemoBytes(model, path_eng);
            Console.ReadKey();
        }

        public static void DemoBytes(Model model, string a_FileName)
        {
            //SpkModel spkModel = new SpkModel("model-spk");
            VoskRecognizer rec = new VoskRecognizer(model, 16000.0f);//, spkModel);
            rec.SetMaxAlternatives(0);
            rec.SetWords(true);
            using (Stream source = File.OpenRead(a_FileName))
            {
                byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    if (rec.AcceptWaveform(buffer, bytesRead))
                    {
                        Console.WriteLine(rec.Result());
                    }
                    else
                    {
                        Console.WriteLine(rec.PartialResult());
                    }
                }
            }

            string result = rec.Result();
            string final_result = rec.FinalResult();
            string win = "yes";
            Console.WriteLine(rec.FinalResult());
        }
    }
}
