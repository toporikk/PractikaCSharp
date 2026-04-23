class SmartAssistant : ITextAssistant, IVoiceAssistant
{
    void ITextAssistant.Respond(string query)
    {
        Console.WriteLine($"[Текстовый ответ]: На ваш запрос '{query}' подготовлена статья.");
    }

    void IVoiceAssistant.Respond(string query)
    {
        Console.WriteLine($"[Голосовой ответ]: Синтезирую аудио для запроса '{query}'...");
    }
}
