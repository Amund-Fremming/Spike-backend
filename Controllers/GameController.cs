using Services;

namespace Controllers;

public class GameController
{
    public readonly GameService _gameService;
    public readonly QuestionService _questionService;

    public GameController(GameService gameService, QuestionService questionService)
    {
        _gameService = gameService;
        _questionService = questionService;
    }
}