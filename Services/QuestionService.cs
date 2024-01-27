using Models;
using Repositories;

namespace Services;

public class QuestionService(QuestionRepository questionRepo, GameRepository gameRepository)
{
    public readonly QuestionRepository _questionRepo = questionRepo;
    public readonly GameRepository _gameRepository = gameRepository;

    public async Task<ICollection<Question>> GetGameQuestionsAsync(string gameId)
    {
        Game game = await _gameRepository.GetGameById(gameId) ?? throw new KeyNotFoundException($"Game with ID {gameId}, does not exist!");

        return await _questionRepo.GetGameQuestionsByGameId(gameId);
    }

    public async Task AddQuestionAsyncTransaction(Question question)
    {
        Game game = await _gameRepository.GetGameById(question.GameId) ?? throw new KeyNotFoundException($"Game with ID {question.GameId}, does not exist!");

        await _questionRepo.AddQuestionToGame(question);
    }

    public async Task<int> GetNumberOfQuestions(string gameId)
    {
        Game game = await _gameRepository.GetGameById(gameId) ?? throw new KeyNotFoundException($"Game with ID {gameId}, does not exist!");

        return _questionRepo.GetNumberOfQuestions(gameId);
    }

}