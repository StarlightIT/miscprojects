using System;
using System.IO;
using RobotCleaner.Console;
using Xunit;

namespace RobotCleaner.Tests
{
    public class TextRunnerTests
    {
        private readonly MemoryStream _inputMemoryStream;
        private readonly MemoryStream _outputMemoryStream;
        private readonly StreamWriter _standardOutWriter;
        private readonly StreamReader _standardOutReader;
        private readonly StreamWriter _standardInWriter;
        private readonly StreamReader _standardInReader;

        private readonly TextRunner _textRunner;
        
        public TextRunnerTests()
        {
            _inputMemoryStream = new MemoryStream();
            _outputMemoryStream = new MemoryStream();
            
            _standardOutReader = new StreamReader(_outputMemoryStream);
            _standardOutWriter = new StreamWriter(_outputMemoryStream);
            
            _standardInReader = new StreamReader(_inputMemoryStream);
            _standardInWriter = new StreamWriter(_inputMemoryStream);
            
            _textRunner = new TextRunner(_standardInReader, _standardOutWriter);
        }

        [Fact]
        public void RunExampleSession()
        {
            _standardInWriter.WriteLine("2");
            _standardInWriter.WriteLine("10 22");
            _standardInWriter.WriteLine("E 2");
            _standardInWriter.WriteLine("N 1");
            _standardInWriter.Flush();
            _inputMemoryStream.Position = 0;
            _textRunner.RunCleaningSession();
            _standardOutWriter.Flush();
            _outputMemoryStream.Position = 0;
            var result = _standardOutReader.ReadLine();
            Assert.Equal("=> Cleaned: 4", result);
        }
    }
}