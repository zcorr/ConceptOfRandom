using System;

public class ReturnToMainMenuException : Exception
{
    public ReturnToMainMenuException() : base("Returning to the main menu.") { }
}