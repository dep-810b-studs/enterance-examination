using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjectOrientedProgramming
{
    public interface IKnowledgeBase
    {
        void addArticle(string article);
        void addTag(string article, string tag);
        void removeTag(string article, string tag);
        List<string> getAllTags();
        List<string> getArticles(string tag);
    }
    
    public class KnowledgeBase : IKnowledgeBase
    {
        private readonly Dictionary<string, List<string>> _articlesWithTags = new ();

        public void addArticle(string article)
        {
            ValidateArticleTitle(article);
            _articlesWithTags.Add(article, new List<string>());
        }

        public void addTag(string article, string tag)
        {
            ValidateArticleTitle(article);
            ValidateTag(tag);
            CheckArticleExisting(article);
            
            _articlesWithTags[article].Add(tag);
        }

        public void removeTag(string article, string tag)
        {
            ValidateArticleTitle(article);
            ValidateTag(tag);
            CheckArticleExisting(article);

            _articlesWithTags[article].Remove(tag);
        }

        public List<string> getAllTags()
        {
            var tags = _articlesWithTags
                .SelectMany(articleWithTags => articleWithTags.Value)
                .Distinct();

            return tags.ToList();
        }

        public List<string> getArticles(string tag)
        {
            ValidateTag(tag);

            var articlesWithSpecifiedTag = _articlesWithTags
                .Where(articleWithTags => articleWithTags.Value.Contains(tag))
                .Select(articleWithTags => articleWithTags.Key)
                .OrderBy(article => article);

            return articlesWithSpecifiedTag.ToList();
        }

        private void ValidateArticleTitle(string articleTitle)
        {
            if (articleTitle == string.Empty)
                throw new ArgumentException("Article name cannot be empty");
        }
        
        private void ValidateTag(string tag)
        {
            if (tag == string.Empty)
                throw new ArgumentException("Tag cannot be empty");
        }

        private void CheckArticleExisting(string article)
        {
            if (!_articlesWithTags.ContainsKey(article))
                throw new ArgumentException($"There is no article {article} in knowledge base");
        }
    }
}