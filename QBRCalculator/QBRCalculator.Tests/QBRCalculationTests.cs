using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using QBRCalculator.BLL;

namespace QBRCalculator.BLL.Tests
{
    [TestFixture]
    public class QBRCalculationTests 
    {     
        [TestCase("Steve Young", 461, 324, 3969, 35, 10, 112.8)]
        [TestCase("Tom Brady", 166, 121, 1635, 12, 1, 125.5)]
        [TestCase("Sam Bradford", 311, 217, 2191, 12, 2, 99.8)]
        [TestCase("Bad Test QB", 100, 30, 300, 0, 30, 0)]
        public void QBRCalculationTest(string name, decimal passesAttempted, decimal passesCompleted, decimal passingYards, decimal touchdownPasses, decimal interceptions, decimal expectedQBR)
        {
            QBRRequest request = new QBRRequest()
            {
                Name = name,
                PassesAttempted = passesAttempted,
                PassesCompleted = passesCompleted,
                PassingYards = passingYards,
                Interceptions = interceptions,
                TouchdownPasses = touchdownPasses
            };
            QBRResponse response = QBRCalculations.CalculateQBR(request);
            Assert.AreEqual(expectedQBR, response.Rating);
        }

        [TestCase(0, 0, 0, 0, 0, false)] //Zero pass attemps
        [TestCase(100, 101, 1000, 10, 5, false)] //More completions than attempts
        [TestCase(100, 50, 500, 50, 52, false)] //TD Passes + Ints > Pass attempts
        [TestCase(10, 9, 50, 3, 2, false)] //Ints > Incompletions
        [TestCase(10, 9, 50, 10, 1, false)] //TDs > Completions
        [TestCase(10, 7, 50, 5, 5, false)] //TDs + Ints > Completions
        public void CannotEnterInvalidStatsTests(decimal passesAttempted, decimal passesCompleted, decimal passingYards, decimal touchdownPasses, decimal interceptions, bool validInput)
        {
            QBRRequest request = new QBRRequest()
            {
                PassesAttempted = passesAttempted,
                PassesCompleted = passesCompleted,
                PassingYards = passingYards,
                Interceptions = interceptions,
                TouchdownPasses = touchdownPasses
            };
            QBRResponse response = QBRCalculations.CalculateQBR(request);
            Assert.AreEqual(validInput, response.ValidInput);
        }

        [TestCase(1, 1, -10, 0, 0, true)] //Can enter negative pass yards
        public void CanEnterValidStatsTests(decimal passesAttempted, decimal passesCompleted, decimal passingYards, decimal touchdownPasses, decimal interceptions, bool validInput)
        {
            QBRRequest request = new QBRRequest()
            {
                PassesAttempted = passesAttempted,
                PassesCompleted = passesCompleted,
                PassingYards = passingYards,
                Interceptions = interceptions,
                TouchdownPasses = touchdownPasses
            };
            QBRResponse response = QBRCalculations.CalculateQBR(request);
            Assert.AreEqual(validInput, response.ValidInput);
        }
    }
}
