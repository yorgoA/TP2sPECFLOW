using System;
using System.Collections.Generic;

namespace ElectionResultCalculator
{
    public class ElectionResult
    {
        private List<Candidate> candidates;
        private int totalVotes;
        private static List<Candidate> _candidates;
        private static List<int> CandidatesvOTES;

        public class Candidate
        {
            public string Name;
            public int Votes;
            public int Id;

            public Candidate(int id,string name, int votes)
            {
                Id = id;
                Name = name;
                Votes = votes;
                

            }
            
            
        }


        public static List<int> CreatelistCandidates(int nbCandidates)
        {
            _candidates = new List<Candidate>();
            for (int i = 1; i <= nbCandidates; i++)
            {
                var candidate = new Candidate(id: i , name: $"Candidate {i}", votes: 0);
                _candidates.Add(candidate);
            };
            foreach (var candidate in _candidates)
            {
                CandidatesvOTES.Add(candidate.Votes);
           
            }

            return CandidatesvOTES;
        }

        public static void AddVotesForCandidates(int candidateNumber, int percentage)
        {
            foreach (var candidate in _candidates)
            {
                if (candidate.Id == candidateNumber)
                {
                    candidate.Votes = percentage;
                } 
            }
        }
        public static int CalculateElectionResult(List<int> NbVotesOfCandidates)
        {
            int winner = 0;
            int maxVotes = 0;
            List<int> voteCount = NbVotesOfCandidates;
            int count = voteCount.Count();
            for (int i = 0; i < count; i++)
            {
                if (voteCount[i] > maxVotes)
                {
                    maxVotes = voteCount[i];
                    winner = i;
                }
            }
            return winner;
        }
    
    }


}