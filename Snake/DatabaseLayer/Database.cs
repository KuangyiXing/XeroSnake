﻿using System.IO;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DatabaseLayer
{
    public class Database
    {
        private const int MaxNoOfScores = 5;
        private SqlConnection sqlConnection;
        private SqlCommand cmd;
        private SqlDataReader sqlDataReader;
        private string gameType;

        public Database(string gameType)
        {
            sqlConnection = new SqlConnection("Server= localhost; Database= SnakeHighScores; Integrated Security=True;");
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection;
            this.gameType = gameType;
        }

        const string FILENAME = "highscores.txt";

        public int getHighSCore()
        {
            int highScore = 100;
            cmd.CommandText = "SELECT * FROM HighScores where GameType = '" + gameType + "'";
            List<HighScore> highScoreList = new List<HighScore>(5);

            sqlConnection.Open();
            sqlDataReader = cmd.ExecuteReader();

            if (sqlDataReader.HasRows)
            {
                while (sqlDataReader.Read())
                {
                    HighScore tempHSObject = new HighScore();
                    tempHSObject.score = sqlDataReader.GetInt32(2);
                    tempHSObject.name = sqlDataReader.GetString(3);
                    highScoreList.Add(tempHSObject);
                }
            }

            sqlConnection.Close();

            return highScore;
        }

        public bool setHighScore(int highScore)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(FILENAME, FileMode.Create)))
            {
                writer.Write(highScore);
            }
            return true;
        }

    }
}