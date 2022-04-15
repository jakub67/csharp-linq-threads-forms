using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadSort {
    static class Program5 {              //SPOUSTET PRES CTRL A F5!!!!!!!
       
        private static Form7 formular;            
        private static Form7 formular2;            
        private static int[] data3 = { 20, 50, 25, 68, 70, 13, 39, 10, 48, 50, 60, 62, 75, 80, 90 };
        private static int[] data4 = { 20, 50, 25, 68, 70, 13, 39, 10, 48, 50, 60, 62, 75, 80, 90 };
        
        static EventWaitHandle th1Ready = new AutoResetEvent(false); //vl1Ready
        static EventWaitHandle th2Ready = new AutoResetEvent(false);
        static EventWaitHandle th1Go = new AutoResetEvent(false);   //vl1Go
        static EventWaitHandle th2Go = new AutoResetEvent(false);
        static bool end1 = false, end2 = false;
        
        [STAThread]
        
        static void Main() {                   //POZOR PROGRAM JEDE POUZE SPUSTENIM POMOCI CTRL FN F5           
            int[] data = new int[15];
            int[] data2 = new int[15];
            var rand = new Random();

            for (int i = 0; i < 15; i++) {
                data[i] = rand.Next(0, 101);
                data2[i] = data[i];
            }
           
            formular = new Form7(data3);
            formular2 = new Form7(data4);
            formular.StartPosition = FormStartPosition.CenterScreen;
            formular.Show();
            formular2.Show();                        //showdialog() ceka na uzavreni formulare, show() ne!!
            formular.showData(data3, 0, 0);
            formular2.showData(data4, 0, 0);

            new Thread(bubbleSortSimple).Start(); 
            new Thread(bubbleSortAdvenced).Start();


            while (!end1 || !end2) {            
                if (!end1) { th1Ready.WaitOne(); }
                if (!end2) { th2Ready.WaitOne(); }
                th1Go.Set(); th2Go.Set();
                Thread.Sleep(1);            
            }
            formular.Close();
            formular2.Close();

        }

        //[Obsolete]
        private static void bubbleSortSimple() {
            for (int i = 0; i < data3.Length; i++) {
                for (int j = 0; j < data3.Length - 1; j++) {                                                         
                    if (data3[j] > data3[j + 1]) {
                        int pom = data3[j]; data3[j] = data3[j + 1]; data3[j + 1] = pom;
                    }
                    formular.showData(data3, j, j + 1);
                    th1Ready.Set();
                    th1Go.WaitOne();
                    System.Threading.Thread.Sleep(100);                    
                }
            }
            end1 = true;
        }

        //[Obsolete]
        private static void bubbleSortAdvenced() {
            int lastSwapIndex = data4.Length - 1, max = 0;
            do {
                max = 0;
                for (int j = 0; j < lastSwapIndex - 1; j++) {
                    if (data4[j] > data4[j + 1]) {
                        int pom = data4[j]; data4[j] = data4[j + 1]; data4[j + 1] = pom; max = j + 1;
                    }
                    formular2.showData(data4, j, j + 1);
                    th2Ready.Set();
                    th2Go.WaitOne();
                    System.Threading.Thread.Sleep(100);                   
                }
                lastSwapIndex = max;
            } while (lastSwapIndex > 0);
            end2 = true;
        }     
    }
}
