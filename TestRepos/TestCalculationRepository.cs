using MathAPI.Models;
using MathAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathAPITestProject.TestRepos
{
    internal class TestCalculationRepository : ICalculationRepository
    {
        private Dictionary<int, Calculation> _calculations = new Dictionary<int, Calculation>();

        public int SaveCalculation(Calculation calculation)
        {
            _calculations.Add((int)calculation.id, calculation);
            return (int)calculation.id;
        }

        public Calculation GetCalculation(int id)
        {
            _calculations.TryGetValue(id, out var calculation);
            return calculation;
        }

        public void UpdateCalculation(Calculation calculation)
        {
            if (_calculations.ContainsKey((int)calculation.id))
            {
                _calculations[(int)calculation.id] = calculation;
            }
        }

        public void DeleteCalculation(int id)
        {
            _calculations.Remove(id);
        }
    }
}
