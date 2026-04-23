using System;

class Program
{
    static void Main()
    {
        SmartAssistant assistant = new SmartAssistant();

        ITextAssistant textRef = assistant;
        textRef.Respond("Как погода?");

        IVoiceAssistant voiceRef = assistant;
        voiceRef.Respond("Включи музыку");

        ((ITextAssistant)assistant).Respond("Напиши код");
    }
}
