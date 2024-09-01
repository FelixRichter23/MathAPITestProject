using MathAPI.Repositories;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathAPITestProject.TestRepos
{
    internal class TestRelationRepository : IRelationRepository
    {
        private List<(int OriginCalculationId, int DestinationCalculationId)> _relations = new List<(int, int)>();

        public void SaveRelation(int originCalculationID, int destinationCalculationID)
        {
            _relations.Add((originCalculationID, destinationCalculationID));
        }

        public List<int> GetDependents(int destinationCalculationID)
        {
            return _relations
                .Where(r => r.DestinationCalculationId == destinationCalculationID)
                .Select(r => r.OriginCalculationId)
                .ToList();
        }

        public void DeleteRelation(int originCalculationID)
        {
            _relations.RemoveAll(r => r.OriginCalculationId == originCalculationID);
        }
    }
}
