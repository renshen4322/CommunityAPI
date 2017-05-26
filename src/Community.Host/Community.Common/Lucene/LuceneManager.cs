using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Version = Lucene.Net.Util.Version;

namespace Community.Common.Lucene
{
    public class LuceneManager
    {
        private LuceneManager() { }

        private static LuceneManager luceneManager;
        private static IndexWriter indexWriter;
        private static IndexSearcher indexSearcher;
        private static IndexReader indexReader;
        private static readonly object luceneManagerLock = new object();
        private static readonly object indexReaderLock = new object();
        private static readonly object indexWriterLock = new object();
        private static readonly object indexSearchLock = new object();
        public static LuceneManager GetInstance()
        {
            if (null == luceneManager)
            {
                lock (luceneManagerLock)
                {
                    if (null == luceneManager)
                    {
                        luceneManager = new LuceneManager();
                    }
                }
            }
            return luceneManager;
        }

        public IndexWriter GetIndexWriter(Directory directory, Analyzer analyzer = null)
        {
            if (null == directory) throw new ArgumentNullException("directory", "Directory can not be null.");
            if (null == indexWriter)
            {
                lock (indexWriterLock)
                {
                    if (null == indexWriter)
                    {
                        if (IndexWriter.IsLocked(directory))
                            throw new LockObtainFailedException("Directory of index had been locked.");
                        indexWriter = new IndexWriter(directory,
                            analyzer ?? new StandardAnalyzer(Version.LUCENE_30),
                             directory.ListAll().Length < 1,
                            IndexWriter.MaxFieldLength.UNLIMITED);
                    }
                }
            }


            return indexWriter;
        }

        public IndexReader GetIndexReader(Directory directory)
        {
            if (null == directory) throw new ArgumentNullException("directory", "Directory can not be null.");
            if (null == indexReader)
            {
                lock (indexReaderLock)
                {
                    if (null == indexReader)
                    {
                        indexReader = DirectoryReader.Open(directory, true);
                    }
                }
            }
            return indexReader;
        }

        public IndexSearcher GetIndexSearcher(IndexReader indexReader)
        {
            if (null == indexReader) throw new ArgumentNullException("indexReader", "IndexReader can not be null.");
            if (null == indexSearcher)
            {
                lock (indexSearchLock)
                {
                    if (null == indexSearcher)
                    {
                        indexSearcher = new IndexSearcher(indexReader);
                    }
                }
            }
            return indexSearcher;
        }
    }
}
