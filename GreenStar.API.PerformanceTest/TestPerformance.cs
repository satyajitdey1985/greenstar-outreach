using GreenStar.API.Test;
using NBench;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenStar.API.PerformanceTest
{
    class TestPerformance
    {
        [PerfBenchmark(Description = "Performance test method to determine memory usage for this method.",
        NumberOfIterations = 1, RunMode = RunMode.Throughput, TestMode = TestMode.Test)]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.GreaterThan, ByteConstants.ThirtyTwoKb)]
        public void TestStudentRepositorySave()
        {
            TestStudentEntry objTestStudentEntry = new TestStudentEntry();
            objTestStudentEntry.TestDemo();
        }
    }
}
