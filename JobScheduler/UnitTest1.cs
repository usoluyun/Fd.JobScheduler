using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace JobScheduler
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ProcessStart()
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = @"H:\01 website\JobScheduler\ConsoleTest\bin\Debug\ConsoleJob.exe";
            p.StartInfo.Arguments = "a b";   //空格分割
            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;

            p.WaitForExit();
            p.Start();
        }

       
    }
}
