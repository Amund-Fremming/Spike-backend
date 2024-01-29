using Services;

namespace Controllers;

public class GameController(GameService gameService, QuestionService questionService)
{
    public readonly GameService _gameService = gameService;
    public readonly QuestionService _questionService = questionService;
}