using HostelBot.Ui.TelegramBot.Handlers.Filling;

namespace HostelBot.Test;

public class StebByStepFilling_tests
{
    private StepByStepFilling stepByStepFilling;

    private StepByStepFilling.Question[] Questions = 
    {
        new ("Name", "Как вас зовут"),
        new ("Surname", "Ваша фамилия"),
        new ("Age", "Сколько вам лет"),
    };
    
    [SetUp]
    public void Setup()
    {
        stepByStepFilling = new StepByStepFilling(Questions);
    }

    [Test]
    public void SameQuestionWithoutProvidingResponse()
    {
        Assert.That(Questions[0].Caption, Is.EqualTo(stepByStepFilling.GetNextQuestion()));
        Assert.That(Questions[0].Caption, Is.EqualTo(stepByStepFilling.GetNextQuestion()));
        Assert.That(Questions[0].Caption, Is.EqualTo(stepByStepFilling.GetNextQuestion()));
    }
    
    [Test]
    public void QuestionsInCorrectOrder()
    {
        Assert.That(Questions[0].Caption, Is.EqualTo(stepByStepFilling.GetNextQuestion()));
        stepByStepFilling.HandleResponse("q");
        
        Assert.That(Questions[1].Caption, Is.EqualTo(stepByStepFilling.GetNextQuestion()));
        stepByStepFilling.HandleResponse("q");
        
        Assert.That(Questions[2].Caption, Is.EqualTo(stepByStepFilling.GetNextQuestion()));
        stepByStepFilling.HandleResponse("q");
    }
    
    [Test]
    public void SameAnswers()
    {
        Assert.That(Questions[0].Caption, Is.EqualTo(stepByStepFilling.GetNextQuestion()));
        stepByStepFilling.HandleResponse("q1");
        
        Assert.That(Questions[1].Caption, Is.EqualTo(stepByStepFilling.GetNextQuestion()));
        stepByStepFilling.HandleResponse("q2");
        
        Assert.That(Questions[2].Caption, Is.EqualTo(stepByStepFilling.GetNextQuestion()));
        stepByStepFilling.HandleResponse("q3");

        var actual = new Dictionary<string, string>
        {
            { Questions[0].Key, "q1" },
            { Questions[1].Key, "q2" },
            { Questions[2].Key, "q3" },
        };
        
        CollectionAssert.AreEquivalent(stepByStepFilling.Answers, actual);
    }
    
    [Test]
    public void CheckResponseStatus()
    {
        var r1 = stepByStepFilling.HandleResponse("q1");
        Assert.That(r1.progressStatus, Is.EqualTo(StepByStepFilling.CurrentProgressStatus.WrittenDown));
            
        var r2 = stepByStepFilling.HandleResponse("q2");
        Assert.That(r2.progressStatus, Is.EqualTo(StepByStepFilling.CurrentProgressStatus.WrittenDown));
        
        var r3 = stepByStepFilling.HandleResponse("q3");
        Assert.That(r3.progressStatus, Is.EqualTo(StepByStepFilling.CurrentProgressStatus.Completed));
        
        var r5 = stepByStepFilling.HandleResponse("q5");
        Assert.That(r5.progressStatus, !Is.EqualTo(StepByStepFilling.CurrentProgressStatus.Completed));
        
        var r4 = stepByStepFilling.HandleResponse("invalid");
        Assert.That(r4.progressStatus, Is.EqualTo(StepByStepFilling.CurrentProgressStatus.AlreadyCompleted));
    }
}