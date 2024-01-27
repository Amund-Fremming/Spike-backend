using Data;
using Repositories;

namespace Services;

public class QuestionService
{
    public readonly QuestionRepository _questionRepo;

    public QuestionService(QuestionRepository questionRepo)
    {
        _questionRepo = questionRepo;
    }
}