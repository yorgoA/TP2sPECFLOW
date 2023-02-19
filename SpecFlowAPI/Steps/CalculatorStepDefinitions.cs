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
        private int _candidateNumber;
        private int _percentage;
        ElectionResult electionResult = new ElectionResult();

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
                CandidatesvOTES = electionResult.CreateListCandidates(nbCandidates);
            } 
        }
        [Given(@"candidate (.*) has (.*)% of the votes")]
        public void GivenCandidateHasOfTheVotes(int candidateNumber, int percentage)
        {
            _candidateNumber = candidateNumber;
            _percentage = percentage;
            electionResult.AddVotesForCandidate(_candidateNumber, _percentage);
        }
        [Given(@"the second round of election has closed")]
        public void GivenTheSecondRoundOfElectionHasClosed()
        {
            _electionClosed = true;
        }

        [Given(@"both candidates have (.*)% of the votes each")]
        public void GivenBothCandidatesHaveOfTheVotesEach(int percentage)
        {
            electionResult.CreateListCandidates(2);
            electionResult.AddVotesForCandidate(1, percentage);
            electionResult.AddVotesForCandidate(2, percentage);
        } 


        [When(@"I calculate the result of the election")]
        public void WhenICalculateTheResultOfTheElection()
        {
            winner= electionResult.CalculateElectionResult(CandidatesvOTES);
        }
        
        [When(@"I calculate the result of the second round of election")]
        public void WhenICalculateTheResultOfTheSecondRoundOfElection()
        {
            winner = electionResult.CalculateElectionResult(CandidatesvOTES);

        }

        [Then(@"candidate (.*) should be declared the winner")]
        public void ThenCandidateShouldBeDeclaredTheWinner(int candidate)
        {
            winner.Should().Be(candidate); 
        }
        [Then(@"a second round of election should be held")]
        public void ThenASecondRoundOfElectionShouldBeHeld()
        {
            electionResult.ShouldHoldSecondRound().Should().BeTrue();
        }

        [Then(@"only candidate (.*) and (.*) should participate in the second round")]
        public void ThenOnlyCandidateAndShouldParticipateInTheSecondRound(int candidate1, int candidate2)
        {
            electionResult.CandidatesInSecondRound().Should().BeEquivalentTo(new List<int> { candidate1, candidate2 });
            electionResult.CandidatesInSecondRound().Count.Should().Be(2);
        }
        [Then(@"no winner can be determined")]
        public void ThenNoWinnerCanBeDetermined()
        {
            winner.Should().Be(0);
        }
    }


}
