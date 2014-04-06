using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsyncApplication
{
    public class Test
    {
        private readonly List<String> websiteList; 
        
        public Test()
        {
            this.websiteList = new List<string>()
            {
                "http://twitter.com/",
                "http://www.redbullshop.com.br/",
                //"http://www.walmart.com.br/",
                //"http://googleblog.blogspot.com.br/"
            };   
        }

        public void DoTest(int threadNumber)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Util.WriteLog("Synchronous Thread started #" + threadNumber, threadNumber);

            SaveWebImage saveWebImage = new SaveWebImage(threadNumber);
            saveWebImage.Do(this.websiteList);

            Util.WriteLog("Synchronous Thread finished #" + threadNumber, threadNumber, stopWatch);
        }

        public async Task DoTestAsync(int threadNumber)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            Util.WriteLog("Asynchronous Thread started #" + threadNumber, threadNumber);

            SaveWebImageAsync saveWebImageAsync = new SaveWebImageAsync(threadNumber);
            await saveWebImageAsync.DoAsync(this.websiteList);

            Util.WriteLog("Asynchronous Thread finished #" + threadNumber, threadNumber, stopWatch);
        }

    }
}
