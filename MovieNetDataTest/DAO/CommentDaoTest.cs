using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieNetData;

namespace MovieNetDataTest.DAO
{
    /// <summary>
    /// Unit tests for CommentDao
    /// </summary>
    [TestClass]
    public class CommentDaoTest
    {
        private static MovieNetData.ViewModel.ServiceFacade serviceFacade = null;

        private static readonly int NB_COMMENTS_TOTAL = 6;

        private static readonly int FIND_COMMENT_BY_USERID = 1;
        private static readonly int NB_COMMENTS_BY_USERID = 2;

        private static readonly int FIND_COMMENT_BY_FILMID = 2;
        private static readonly int NB_COMMENTS_BY_FILMID = 2;

        [TestInitialize]
        public void Init()
        {
            serviceFacade = MovieNetData.ViewModel.ServiceFacade.Instance;
        }

        [TestMethod]
        public void TestFindAllComments()
        {
            List<Comment> allComments = serviceFacade.CommentDao.FindAllComments;

            if (allComments.Count > 0)
            {
                Assert.AreEqual(NB_COMMENTS_TOTAL, allComments.Count);
            }
            else Assert.Fail("No comment was found.");
        }

        [TestMethod]
        public void TestFindCommentByCriteria()
        {
            List<Comment> commentsByUserId = serviceFacade.CommentDao.FindCommentsByUserId(FIND_COMMENT_BY_USERID);
            List<Comment> commentsByFilmId = serviceFacade.CommentDao.FindCommentsByFilmId(FIND_COMMENT_BY_FILMID);

            Assert.IsNotNull(commentsByUserId);
            Assert.AreEqual(NB_COMMENTS_BY_USERID, commentsByUserId.Count);

            Assert.IsNotNull(commentsByFilmId);
            Assert.AreEqual(NB_COMMENTS_BY_FILMID, commentsByFilmId.Count);
        }

        //[TestMethod]
        public void TestCRUDComment()
        {
            // Create
            User userCRUD = serviceFacade.UserDao.FindUserByUsername("aleparedes00");
            Film film = serviceFacade.FilmDao.findById(1);
            Comment commentCRUD = new Comment("I liked the movie", userCRUD, film);
            Console.WriteLine("commentCRUD : " + commentCRUD);
            Console.WriteLine("commentDAO : " + serviceFacade.CommentDao);
            commentCRUD = serviceFacade.CommentDao.CreateComment(commentCRUD);
            Assert.IsNotNull(commentCRUD);
            Assert.IsNotNull(commentCRUD.Id);
            Assert.IsNotNull(serviceFacade.CommentDao.FindCommentById(commentCRUD.Id));

            // Update
            commentCRUD.Text = "test2";
            Comment commentCRUD2 = serviceFacade.CommentDao.UpdateComment(commentCRUD);
            Assert.IsNotNull(commentCRUD2);
            Assert.AreEqual("test2", commentCRUD2.Text);

            // Delete
            Assert.IsTrue(serviceFacade.CommentDao.DeleteComment(commentCRUD));
            Assert.IsNull(serviceFacade.CommentDao.FindCommentById(commentCRUD.Id));
              List<Comment> finalComments = serviceFacade.CommentDao.FindAllComments;
            if (finalComments != null)
            {
                Assert.AreEqual(NB_COMMENTS_TOTAL, finalComments.Count);
            }
            else Assert.Fail();
        }
    }
}
