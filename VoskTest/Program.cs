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
            string path_eng = "E:\\Artem\\Vosk\\Wave\\red.wav";
            string model_eng = "E:\\Artem\\Vosk\\Models\\vosk-model-en-us-0.20";

            Vosk.Model model = new Model(model_eng);
            DemoFloats(model, path_eng);
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

            Console.WriteLine(rec.FinalResult());
        }

        public static void DemoFloats(Model model, string a_FileName)
        {
            // Demo float array
            VoskRecognizer rec = new VoskRecognizer(model, 16000.0f);
            using (Stream source = File.OpenRead(a_FileName))
            {
                byte[] buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                {
                    float[] fbuffer = new float[bytesRead / 2];
                    for (int i = 0, n = 0; i < fbuffer.Length; i++, n += 2)
                    {
                        fbuffer[i] = (short)(buffer[n] | buffer[n + 1] << 8);
                    }
                    if (rec.AcceptWaveform(fbuffer, fbuffer.Length))
                    {
                        Console.WriteLine(rec.Result());
                    }
                    else
                    {
                        Console.WriteLine(rec.PartialResult());
                    }
                }
            }
            Console.WriteLine(rec.FinalResult());
        }
    }
}
