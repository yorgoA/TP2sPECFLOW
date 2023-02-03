using System;
using System.Collections;
using TechTalk.SpecFlow;
using System.Reflection.Metadata.Ecma335;
using FluentAssertions;
using ElectionResultCalculator;

namespace ElectionResultCalculation.Specs
{
    [Binding]
    public class ElectionResultCalculationSteps
    {

        private List<int> CandidatesvOTES;
        private  int winner;
        private List<ElectionResult.Candidate> _candidates;
        private bool _electionClosed;
        

        [Given(@"the election has closed")]
        public void GivenTheElectionHasClosed()
        {
            _electionClosed = true;
        }

        [Given(@"there are (.*) candidates")]
        public void GivenThereAreCandidates(int nbCandidates)
        {
            if (_electionClosed==true)
            {
                CandidatesvOTES =  ElectionResult.CreatelistCandidates(nbCandidates);
            } 
            
         
        }
        [Given(@"candidate (.*) has (.*)% of the votes")]
        public static void GivenCandidateHasOfTheVotes(int candidateNumber, int percentage)
        {
          ElectionResult.AddVotesForCandidates(candidateNumber,percentage);

        }

        [When(@"I calculate the result of the election")]
        public void WhenICalculateTheResultOfTheElection()
        {
            winner= ElectionResult.CalculateElectionResult(CandidatesvOTES);
        }
        


        [Then(@"candidate (.*) should be declared the winner")]
        public void ThenCandidateShouldBeDeclaredTheWinner(int candidate)
        {
            winner.Should().Be(candidate); 
        }
    }


}
