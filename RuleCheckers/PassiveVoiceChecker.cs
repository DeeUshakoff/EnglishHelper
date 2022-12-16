using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishHelper.RuleCheckers
{
    public class WordBase
    {
        public  List<Word> Words = new List<Word>();
        public void AddWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) return;
            //word = string.Join("", word.Where(x => char.IsLetter(x)));

        } 
        public WordBase()
        {
            
        }
    }
    public class Word
    {
        public readonly string Content;
        public readonly WordType WordType;
        public readonly VerbForm VerbForm;
        public Word(string word)
        {
            Content = word;
            WordType = GetWordType(word);
        }
        public WordType GetWordType(string word)
        {
            if(string.IsNullOrWhiteSpace(word))
                return WordType.Undefined;

            var clearedWord = string.Join("", word.Where(x => char.IsLetter(x)));
            if (clearedWord.Length < word.Length)
                return WordType.Undefined;
            throw new Exception();
        }
    }

   
    public enum WordType
    {
        Noun,
        Verb,
        Adjective,
        Pronoun,
        Adverb,
        Numerals,
        Undefined
    }
    public enum VerbForm
    {
        First,
        Second,
        Third
    }
    
    internal class PassiveVoiceChecker : IRule
    {
        public bool Check(string input)
        {
            throw new NotImplementedException();
        }
    }
    public interface IRule
    {
        bool Check(string input);
        
    }
}
