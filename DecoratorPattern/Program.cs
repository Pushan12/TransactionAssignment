
/*
 * you'll have a TextComponent representing plain text and various decorators that add formatting to the text, such as bold, italic, and underline. 
 * These decorators are stacked to apply multiple formatting options to the text dynamically.
 */
namespace DecoratorPattern
{
    using System;

    // Component interface
    public interface ITextComponent
    {
        string GetText();
    }

    // Concrete component
    public class PlainText : ITextComponent
    {
        private string text;

        public PlainText(string text)
        {
            this.text = text;
        }

        public string GetText()
        {
            return text;
        }
    }

    // Decorator base class
    public abstract class TextDecorator : ITextComponent
    {
        protected ITextComponent textComponent;

        public TextDecorator(ITextComponent textComponent)
        {
            this.textComponent = textComponent;
        }

        public virtual string GetText()
        {
            return textComponent.GetText();
        }
    }

    // Concrete decorators
    public class BoldDecorator : TextDecorator
    {
        public BoldDecorator(ITextComponent textComponent) : base(textComponent) { }

        public override string GetText()
        {
            return "<b>" + base.GetText() + "</b>";
        }
    }

    public class ItalicDecorator : TextDecorator
    {
        public ItalicDecorator(ITextComponent textComponent) : base(textComponent) { }

        public override string GetText()
        {
            return "<i>" + base.GetText() + "</i>";
        }
    }

    public class UnderlineDecorator : TextDecorator
    {
        public UnderlineDecorator(ITextComponent textComponent) : base(textComponent) { }

        public override string GetText()
        {
            return "<u>" + base.GetText() + "</u>";
        }
    }

    public class RichTextEditor
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter sample sentence : ");
            var vall = Console.ReadLine();

            ITextComponent text = new PlainText(vall);

            // Apply decorators
            text = new BoldDecorator(text);
            text = new ItalicDecorator(text);
            text = new UnderlineDecorator(text);

            Console.WriteLine("Formatted Text: " + text.GetText());
        }
    }
}