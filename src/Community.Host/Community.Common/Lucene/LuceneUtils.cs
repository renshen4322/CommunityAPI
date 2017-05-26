using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Directory = Lucene.Net.Store.Directory;
using Version = Lucene.Net.Util.Version;

namespace Community.Common.Lucene
{
    public class LuceneUtils
    {
        private static readonly LuceneManager luceneManager = LuceneManager.GetInstance();

        private static readonly Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_30);
        private static int writerIndex = 0;
        private const int MAX_COMMIT_LENGTH = 5;

        /// <summary>
        /// 提交writer上的修改
        /// </summary>
        /// <param name="writer"></param>
        private static void WriterCommit(IndexWriter writer)
        {
            LuceneUtils.writerIndex += 1;
            if (LuceneUtils.writerIndex > LuceneUtils.MAX_COMMIT_LENGTH)
            {
                writer.Commit();
                LuceneUtils.writerIndex = 0;
            }
        }
        /// <summary>
        ///  提交writer上的修改
        /// </summary>
        /// <param name="writer"></param>
        public static void Commit(IndexWriter writer)
        {
            writer.Commit();
        }
        /// <summary>
        /// 打开索引目录
        /// </summary>
        /// <param name="luceneDir">索引目录地址</param>
        /// <returns></returns>
        public static FSDirectory OpenFSDirectory(string luceneDir)
        {
            if (!System.IO.Directory.Exists(luceneDir))
            {
                System.IO.Directory.CreateDirectory(luceneDir);
            }
            FSDirectory directory = FSDirectory.Open(luceneDir);
            return directory;
        }
        /// <summary>
        /// 关闭索引目录
        /// </summary>
        /// <param name="directory">待关闭目录</param>
        public static void CloseFSDirectory(Directory directory)
        {
            if (directory != null)
            {
                directory.Dispose();
            }
        }
        /// <summary>
        /// 获取IndexWriter
        /// </summary>
        /// <param name="directoryPath">索引所在目录路径</param>
        /// <returns></returns>
        public static IndexWriter GetIndexWriter(string directoryPath)
        {
            Directory directory = OpenFSDirectory(directoryPath);
            return luceneManager.GetIndexWriter(directory);
        }
        /// <summary>
        /// 获取IndexWriter
        /// </summary>
        /// <param name="directory">索引目录对象</param>
        /// <returns></returns>
        public static IndexWriter GetIndexWriter(Directory directory)
        {
            return luceneManager.GetIndexWriter(directory);
        }
        /// <summary>
        /// 获取IndexReader
        /// </summary>
        /// <param name="directory">索引目录对象</param>
        /// <returns></returns>
        public static IndexReader GetIndexReader(Directory directory)
        {
            return luceneManager.GetIndexReader(directory);
        }
        /// <summary>
        /// 获取IndexSearcher
        /// </summary>
        /// <param name="reader">IndexReader实例</param>
        /// <returns></returns>
        public static IndexSearcher GetIndexSearcher(IndexReader reader)
        {
            return luceneManager.GetIndexSearcher(reader);
        }
        /// <summary>
        ///  获取IndexSearcher
        /// </summary>
        /// <param name="directory">索引目录实例</param>
        /// <returns></returns>
        public static IndexSearcher GetIndexSearcher(Directory directory)
        {
            return luceneManager.GetIndexSearcher(GetIndexReader(directory));
        }
        /// <summary>
        ///  获取IndexSearcher
        /// </summary>
        /// <param name="directoryPath">索引所在目录路径</param>
        /// <returns></returns>
        public static IndexSearcher GetIndexSearcher(string directoryPath)
        {
            return luceneManager.GetIndexSearcher(GetIndexReader(OpenFSDirectory(directoryPath)));
        }
        /// <summary>
        /// 关闭IndexReader
        /// </summary>
        /// <param name="reader"></param>
        public static void CloseIndexReader(IndexReader reader)
        {
            if (null != reader)
            {
                try
                {
                    reader.Dispose();
                    reader = null;
                }
                catch (IOException e)
                {
                    throw e;
                }
            }
        }
        /// <summary>
        /// 关闭IndexWriter
        /// </summary>
        /// <param name="writer"></param>
        public static void CloseIndexWriter(IndexWriter writer)
        {
            if (null != writer)
            {
                try
                {
                    writer.Dispose();
                    writer = null;
                }
                catch (IOException e)
                {
                    throw e;
                }
            }
        }
        /// <summary>
        /// 关闭IndexReader和IndexWriter 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="writer"></param>
        public static void CloseAll(IndexReader reader, IndexWriter writer)
        {
            CloseIndexReader(reader);
            CloseIndexWriter(writer);
        }
        /// <summary>
        /// 删除索引
        /// 需手动关闭IndexWriter
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        public static void DeleteIndex(IndexWriter writer, string field, string value)
        {
            try
            {
                writer.DeleteDocuments(new Term[] { new Term(field, value) });
                WriterCommit(writer);
            }
            catch (IOException e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 删除索引
        /// 需手动关闭IndexWriter
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="query"></param>
        public static void DeleteIndex(IndexWriter writer, Query query)
        {
            try
            {
                writer.DeleteDocuments(query);
                WriterCommit(writer);
            }
            catch (IOException e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 批量删除索引
        /// 需手动关闭IndexWriter
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="terms"></param>
        public static void DeleteIndexs(IndexWriter writer, Term[] terms)
        {
            try
            {
                writer.DeleteDocuments(terms);
                WriterCommit(writer);
            }
            catch (IOException e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 批量删除索引
        /// 需手动关闭IndexWriter
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="querys"></param>
        public static void DeleteIndexs(IndexWriter writer, Query[] querys)
        {
            try
            {
                writer.DeleteDocuments(querys);
                WriterCommit(writer);
            }
            catch (IOException e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 删除所有索引文档
        /// </summary>
        /// <param name="writer"></param>
        public static void DeleteAllIndex(IndexWriter writer)
        {
            try
            {
                writer.DeleteAll();
                WriterCommit(writer);
            }
            catch (IOException e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 更新索引文档
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="term"></param>
        /// <param name="document"></param>
        public static void UpdateIndex(IndexWriter writer, Term term, Document document)
        {
            try
            {
                writer.UpdateDocument(term, document);
                WriterCommit(writer);
            }
            catch (IOException e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 更新索引文档
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="document"></param>
        public static void UpdateIndex(IndexWriter writer, String field, String value, Document document)
        {
            UpdateIndex(writer, new Term(field, value), document);
        }
        /// <summary>
        /// 添加索引
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="document"></param>
        public static void AddIndex(IndexWriter writer, Document document)
        {
            try
            {
                writer.AddDocument(document);
                WriterCommit(writer);
            }
            catch (IOException e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 索引文档查询
        /// </summary>
        /// <param name="searcher"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static List<Document> Query(IndexSearcher searcher, Query query)
        {
            List<Document> documents = null;
            TopDocs topDocs;
            try
            {
                topDocs = searcher.Search(query, int.MaxValue);
            }
            catch (IOException e)
            {
                throw e;
            }
            ScoreDoc[] scores = topDocs.ScoreDocs;
            int length = scores.Length;
            if (length > 0)
            {
                documents = new List<Document>();
                try
                {
                    for (int i = 0; i < length; i++)
                    {
                        Document doc = searcher.Doc(scores[i].Doc);
                        documents.Add(doc);
                    }
                }
                catch (IOException e)
                {
                    throw e;
                }
            }

            return documents;
        }
        /// <summary>
        /// 返回索引文档的总数[注意：请自己手动关闭IndexReader] 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static int GetIndexTotalCount(IndexReader reader)
        {
            return reader.NumDocs();
        }
        /// <summary>
        /// 返回索引文档中最大文档ID[注意：请自己手动关闭IndexReader] 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static int GetMaxDocId(IndexReader reader)
        {
            return reader.MaxDoc;
        }
        /// <summary>
        /// 返回已经删除尚未提交的文档总数[注意：请自己手动关闭IndexReader] 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static int GetDeletedDocNum(IndexReader reader)
        {
            return GetMaxDocId(reader) - GetIndexTotalCount(reader);
        }
        public static Document FindDocumentByDocId(IndexReader reader, int docId)
        {
            try
            {
                return reader.Document(docId);
            }
            catch (IOException e)
            {
                return null;
            }
        }
        /// <summary>
        /// 获取符合条件的总记录数 
        /// </summary>
        /// <param name="search"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static int searchTotalRecord(IndexSearcher search, Query query, Sort sort=null)
        {
            int docs = 0;
            try
            {
                TopDocs topDocs;
                if (sort != null)
                {
                    topDocs = search.Search(query, null, int.MaxValue, sort);
                }
                else
                {
                    topDocs = search.Search(query, int.MaxValue);
                }
                if (topDocs != null || topDocs.ScoreDocs != null || topDocs.ScoreDocs.Length != 0)
                    docs = topDocs.ScoreDocs.Length;
            }
            catch (IOException e)
            {
                throw e;
            }
            return docs;
        }

        /// <summary>
        ///  Lucene分页查询 
        /// </summary>
        /// <param name="searcher"></param>
        /// <param name="directory"></param>
        /// <param name="query">查询条件</param>
        /// <param name="pageIndex">page页码[从1开始]</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="sort">排序规则</param>
        /// <returns></returns>
        public static List<Document> pageQuery(IndexSearcher searcher, Query query, int pageIndex, int pageSize,Sort sort)
        {

           // Sort sort = new Sort(new SortField("WorksCreatedAt", SortField.DOC, false));
            List<Document> documents = null;
            
            int start = (pageIndex - 1) * pageSize;
            int end = pageIndex * pageSize;
           

            TopDocs topDocs = null;
            try
            {
                topDocs = searcher.Search(query, null, end, sort);
            }
            catch (IOException e)
            {
                throw e;
            }

            ScoreDoc[] docs = topDocs.ScoreDocs;
            if (docs.Length > start)
            {
                documents = new List<Document>();
                Document doc;
                for (int i = start; i < end; i++)
                {
                    if (i>=docs.Length)
                    {
                        continue;    
                    }
                    doc = searcher.Doc(docs[i].Doc);
                    documents.Add(doc);
                }
            }
            return documents;
        }
    }
}
