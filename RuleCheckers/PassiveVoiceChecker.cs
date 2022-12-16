using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Text;

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
            return WordType.Undefined;
        }
        public override string ToString()
        {
            return Content;
        }
    }
    public class Sentence
    {
        public Time Time { get; private set; }
        public  TimeForm TimeForm { get; private set; }

        public  List<Word> words { get; private set; } = new List<Word>();

        public bool IsPassive { get; private set; }
        public Sentence(string sentence)
        {
            if (string.IsNullOrWhiteSpace(sentence))
                throw new Exception("Empty sentence");
            var rawWords = sentence.Split('.').First().Where(x => char.IsLetter(x) || x == ' ');
            words = string.Join("", rawWords).Split(" ").Select(x => new Word(x)).ToList();
            CheckSentence(this);
        }
        public override string ToString()
        {
            return string.Join(" ", words);
        }
        private static string[] Verbs = new string[]
        {
            
        };
        static Sentence()
        {
            Verbs = File.ReadAllLines($"{Directory.GetCurrentDirectory()}"+@"\DataBase\Words\Verb3WordList.txt").Select(x => x.ToLower()).ToArray();
            //App.DisplayNotification(Verbs.Length.ToString());
        }
        public static void CheckSentence(Sentence sentence)
        {
            var sent = sentence.ToString().ToLower();
            bool a = true;


            // Present Simple
            string[] prs = new[] { "am", "is", "are" };

            for (int i = 0; i < prs.Length; i++)
            {
                bool x = false;
                while (sent.Contains(prs[i]) == false)
                {
                    if (i < prs.Length - 1)
                    {
                        i++;
                    }
                    else
                    {
                        x = true;
                        break;
                    }
                }

                for (int j = 0; j < Verbs.Length; j++)
                {
                    while (sent.Contains(Verbs[j]))
                    {
                        if (j < Verbs.Length - 1)
                        {
                            j++;
                        }
                        else
                        {
                            a = false;
                            break;
                        }
                    }

                    if (a == true && x == false)
                    {
                        sentence.Time = Time.Present;
                        sentence.TimeForm = TimeForm.Simple;
                        sentence.IsPassive = true;
                    }
                    break;

                }
                break;
            }

            a = true;

            //Past Simple
            string[] ps = new[] { "was", "were" };

            for (int i = 0; i < ps.Length; i++)
            {
                bool x = false;
                while (sent.Contains(ps[i]) == false)
                {
                    if (i < ps.Length - 1)
                    {
                        i++;
                    }
                    else
                    {
                        x = true;
                        break;
                    }
                }

                for (int j = 0; j < Verbs.Length; j++)
                {
                    while (sent.Contains(Verbs[j]))
                    {
                        if (j < Verbs.Length - 1)
                        {
                            j++;
                        }
                        else
                        {
                            a = false;
                            break;
                        }
                    }

                    if (a == true && x == false)
                    {
                        sentence.Time = Time.Past;
                        sentence.TimeForm = TimeForm.Simple;
                        sentence.IsPassive = true;

                    }
                    break;

                }
                break;
            }

            a = true;

            //Future Simple 
            string fs = "will be";
            if (sent.Contains(fs))
            {
                for (int j = 0; j < Verbs.Length; j++)
                {
                    bool x = false;
                    while (sent.Contains(Verbs[j]))
                    {
                        if (j < Verbs.Length - 1)
                        {
                            j++;
                        }
                        else
                        {
                            x = true;
                            break;
                        }
                    }
                    if (a == true && x == false)
                    {
                        sentence.Time = Time.Future;
                        sentence.TimeForm = TimeForm.Simple;
                        sentence.IsPassive = true;
                    }
                    break;
                }

            }

            a = true;

            //Present Continious
            string[] prc = new string[] { "am being", "is being", "are being" };

            for (int i = 0; i < prc.Length; i++)
            {
                bool x = false;
                while (sent.Contains(prc[i]) == false)
                {
                    if (i < prc.Length - 1)
                    {
                        i++;
                    }
                    else
                    {
                        x = true;
                        break;
                    }
                }

                for (int j = 0; j < Verbs.Length; j++)
                {
                    while (sent.Contains(Verbs[j]))
                    {
                        if (j < Verbs.Length - 1)
                        {
                            j++;
                        }
                        else
                        {
                            a = false;
                            break;
                        }
                    }

                    if (a == true && x == false)
                    {
                        sentence.Time = Time.Present;
                        sentence.TimeForm = TimeForm.Continuous;
                        sentence.IsPassive = true;
                    }
                    break;

                }
                break;
            }

            a = true;

            //Past Continious
            string[] pc = new[] { "was being", "were being" };

            for (int i = 0; i < pc.Length; i++)
            {
                bool x = false;
                while (sent.Contains(pc[i]) == false)
                {
                    if (i < pc.Length - 1)
                    {
                        i++;
                    }
                    else
                    {
                        x = true;
                        break;
                    }
                }

                for (int j = 0; j < Verbs.Length; j++)
                {
                    while (sent.Contains(Verbs[j]))
                    {
                        if (j < Verbs.Length - 1)
                        {
                            j++;
                        }
                        else
                        {
                            a = false;
                            break;
                        }
                    }

                    if (a == true && x == false)
                    {
                        sentence.Time = Time.Past;
                        sentence.TimeForm = TimeForm.Continuous;
                        sentence.IsPassive = true;
                   
                    }
                    break;

                }
                break;
            }

            a = true;

            //Present Perfect
            string[] prp = new[] { "has been", "have been" };

            for (int i = 0; i < prp.Length; i++)
            {
                bool x = false;
                while (sent.Contains(prp[i]) == false)
                {
                    if (i < prp.Length - 1)
                    {
                        i++;
                    }
                    else
                    {
                        x = true;
                        break;
                    }
                }

                for (int j = 0; j < Verbs.Length; j++)
                {
                    while (sent.Contains(Verbs[j]))
                    {
                        if (j < Verbs.Length - 1)
                        {
                            j++;
                        }
                        else
                        {
                            a = false;
                            break;
                        }
                    }

                    if (a == true && x == false)
                    {
                        sentence.Time = Time.Present;
                        sentence.TimeForm = TimeForm.Perfect;
                        sentence.IsPassive = true;
                    }
                    break;

                }
                break;
            }

            a = true;

            //Past Perfect
            string pp = "had been";

            if (sent.Contains(pp))
            {
                for (int j = 0; j < Verbs.Length; j++)
                {
                    bool x = false;
                    while (sent.Contains(Verbs[j]))
                    {
                        if (j < Verbs.Length - 1)
                        {
                            j++;
                        }
                        else
                        {
                            x = false;
                            break;
                        }
                    }
                    if (a == true && x == false)
                    {
                        sentence.Time = Time.Past;
                        sentence.TimeForm = TimeForm.Perfect;
                        sentence.IsPassive = true;
                       
                    }
                    break;
                }

            }

            a = true;

            //Future Perfect
            string[] fp = new string[] { "will has been", "will have been" };

            for (int i = 0; i < fp.Length; i++)
            {
                bool x = false;
                while (sent.Contains(fp[i]) == false)
                {
                    if (i < fp.Length - 1)
                    {
                        i++;
                    }
                    else
                    {
                        x = true;
                        break;
                    }
                }

                for (int j = 0; j < Verbs.Length; j++)
                {
                    while (sent.Contains(Verbs[j]))
                    {
                        if (j < Verbs.Length - 1)
                        {
                            j++;
                        }
                        else
                        {
                            a = false;
                            break;
                        }
                    }

                    if (a == true && x == false)
                    {
                        sentence.Time = Time.Future;
                        sentence.TimeForm = TimeForm.Perfect;
                        sentence.IsPassive = true;
                        
                    }
                    break;

                }
                break;
            }
            //sentence.IsPassive = !a;
            
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
    public enum Time
    {
        Undefined,
        Present,
        Past,
        Future
    }
    public enum TimeForm
    {
        Undefined,
        Simple,
        Continuous,
        Perfect

    }
    
    public class PassiveVoiceChecker : IRule
    {
        public bool Check(string input)
        {
            throw new NotImplementedException();
        }
        public bool Check(Sentence input)
        {
            throw new NotImplementedException();
        }
    }
    public interface IRule
    {
        bool Check(string input);
        
    }
}

public static class FindTime
{
    
}