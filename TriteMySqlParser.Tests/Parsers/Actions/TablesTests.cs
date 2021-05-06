using Sprache;
using System;
using System.Collections.Generic;
using System.Text;
using TriteMySqlParser.Parsers;
using TriteMySqlParser.Parsers.Actions;
using TriteMySqlParser.Types;
using Xunit;

namespace TriteMySqlParser.Tests.Parsers.Actions
{
    public class TablesTests
    {
        [Fact]
        public void DropTableIfExists_ShouldParseDropTableLine()
        {
            Assert.Equal("sometable01", Tables.DropTableIfExists.Parse("DROP TABLE IF EXISTS `sometable01`;").Name);
        }

        [Fact]
        public void CreateTable_ShouldMostlyWorkRightNowIThink()
        {
            var testString = @"
                CREATE TABLE `sometable01` (
                    `idsometable01` int(11) NOT NULL AUTO_INCREMENT,
                    `sometable01col01` varchar(45) DEFAULT NULL,
                    `sometable01col02` int(11) DEFAULT NULL,
                    `sometable01col03` datetime DEFAULT NULL,
                    `sometable01col04` double DEFAULT NULL,
                    PRIMARY KEY (`idsometable01`),
                    UNIQUE KEY `idsometable01_UNIQUE` (`idsometable01`)
                ) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;
            ";

            var test = Tables.CreateTable.End().Parse(testString);

            DataPoolGlobal.Pool.Reset();
            test.Execute();
        }

        [Fact]
        public void DoesThisWorkSoFar()
        {
            // This was mostly to see how things were going, not a valid test right now and needs reworking for later.
            var testString = @"
                -- MySQL dump 10.13  Distrib 5.7.30, for Win64 (x86_64)
                --
                -- Host: localhost    Database: trite_test_schema
                -- ------------------------------------------------------
                -- Server version	5.7.30-log

                /*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
                /*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
                /*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
                /*!40101 SET NAMES utf8 */;
                /*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
                /*!40103 SET TIME_ZONE='+00:00' */;
                /*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
                /*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
                /*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
                /*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

                --
                -- Table structure for table `sometable01`
                --

                DROP TABLE IF EXISTS `sometable01`;
                /*!40101 SET @saved_cs_client     = @@character_set_client */;
                /*!40101 SET character_set_client = utf8 */;
                CREATE TABLE `sometable01` (
                  `idsometable01` int(11) NOT NULL AUTO_INCREMENT,
                  `sometable01col01` varchar(45) DEFAULT NULL,
                  `sometable01col02` int(11) DEFAULT NULL,
                  `sometable01col03` datetime DEFAULT NULL,
                  `sometable01col04` double DEFAULT NULL,
                  PRIMARY KEY (`idsometable01`),
                  UNIQUE KEY `idsometable01_UNIQUE` (`idsometable01`)
                ) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;
                /*!40101 SET character_set_client = @saved_cs_client */;

                --
                -- Dumping data for table `sometable01`
                --

                LOCK TABLES `sometable01` WRITE;
                /*!40000 ALTER TABLE `sometable01` DISABLE KEYS */;
                INSERT INTO `sometable01` VALUES (1,'something001',42,'2020-10-21 09:43:00',123.45),(2,'something002',27,'2020-10-21 09:44:00',485.584);
                /*!40000 ALTER TABLE `sometable01` ENABLE KEYS */;
                UNLOCK TABLES;
                /*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

                /*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
                /*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
                /*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
                /*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
                /*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
                /*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
                /*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

                -- Dump completed on 2020-10-21 18:30:51
            ";

            var parseEither =
                //Tables.CreateTable
                //.Or(Comments.AnyComment);
                Comments.AnyComment.Token()
                .Or(Tables.DropTableIfExists.Token())
                .Or(Tables.CreateTable.Token())
                .Or(Rows.InsertIntoTable.Token())
                .Or(Tables.LockTables.Token())
                .Or(Tables.UnlockTables.Token());

            var testParser =
                parseEither.Many().End();

            var result = testParser.Parse(testString);

            DataPoolGlobal.Pool.Reset();

            foreach (var command in result)
            {
                command.Execute();
            }
        }

        [Fact]
        public void LockTables_ShouldParseLockTables()
        {
            Assert.Equal("sometable01", Tables.LockTables.Parse("LOCK TABLES `sometable01` WRITE;").Name);
        }
    }
}
