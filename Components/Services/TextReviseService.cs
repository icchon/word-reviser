using DiffMatchPatch;
using Microsoft.Extensions.Primitives;
using System.Diagnostics;

namespace WordReviser.Components.Services
{
    public interface ITextReviseService
    {
        public List<string> ConvertWithLLM();
        public List<string> Sentences { get; }
        public List<List<Diff>> Diffs { get;}
    }
    public class TextReviseService:ITextReviseService
    {
        private readonly IHtmlManageService _htmlManageService;

        private List<string> _sentences = new List<string>();
        public List<string> Sentences => ConvertWithLLM();

        private List<List<Diff>> _diffs = new List<List<Diff>>();
        public List<List<Diff>> Diffs => JudgeDiff();
        public TextReviseService(IHtmlManageService htmlManageService)
        {
            _htmlManageService = htmlManageService;
        }

        private List<List<Diff>> JudgeDiff()
        {
            if(_diffs.Count > 0)
            {
                return _diffs;
            }
            List<string> preSentences = _htmlManageService.Read();
            List<string> postSentences = ConvertWithLLM();

            if(preSentences.Count != postSentences.Count)
            {
                Debug.WriteLine($"not match the length, presentences : {preSentences.Count}, postSentences : {postSentences.Count}");
                return new List<List<Diff>>();
            }

            diff_match_patch dmp = new diff_match_patch();
            int len = preSentences.Count;
            for(int i= 0; i< len ; i++)
            {
                string preSentence = preSentences[i];
                string postSentence = postSentences[i];
                List<Diff> diff = dmp.diff_main(preSentence, postSentence);
                _diffs.Add(diff);
            }

            return _diffs;
        }

        public List<string> ConvertWithLLM()
        {
            {
                //inplement AI revise code 
            }

            if(_sentences.Count > 0)
            {
                return _sentences;
            }

            var random = new Random();
            List<string> preSentences = _htmlManageService.Read();
            List<string> postSentences = new List<string>();
            foreach (string present in preSentences)
            {
                string post = present;
                if(present.Length > 0)
                {
                    post = post + "追加";
                }
                postSentences.Add(post);
            }

            _sentences = postSentences;
            return _sentences;
        }
    }
}
