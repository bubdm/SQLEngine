﻿using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace SQLEngine
{
    public static class IndentedTextWriterExtensions
    {
        public static void WriteWithScoped(this IndentedTextWriter writer, string expression)
        {
            writer.BeginScope();
            writer.Write(expression);
            writer.EndScope();
        }
        public static void BeginScope(this IndentedTextWriter writer)
        {
            writer.Write(SQLKeywords.BEGIN_SCOPE);
        }
        public static void EndScope(this IndentedTextWriter writer)
        {
            writer.Write(SQLKeywords.END_SCOPE);
        }
        public static void WriteLineComment(this IndentedTextWriter writer, string comment)
        {
            writer.WriteComment(comment);
            writer.WriteLine();
        }
        public static void WriteComment(this IndentedTextWriter writer, string comment)
        {
            writer.Write("/*");
            writer.Write(comment);
            writer.Write("*/");
        }
        public static void WriteScoped(this IndentedTextWriter writer, string expression,
            string beginScope = SQLKeywords.BEGIN_SCOPE, string endScope = SQLKeywords.END_SCOPE)
        {
            writer.Write(beginScope);
            writer.Write(expression);
            writer.Write(endScope);
        }
        public static void WriteScoped(this IndentedTextWriter writer, string[] expressionArray)
        {
            var expression = string.Join(" , ", expressionArray);
            writer.Write(SQLKeywords.BEGIN_SCOPE);
            writer.Write(expression);
            writer.Write(SQLKeywords.END_SCOPE);
        }
        public static void WriteJoined(this IndentedTextWriter writer, IEnumerable<string> expressionArray, string joiner = " , ", bool useNewLine = false)
        {
            var first = true;
            foreach (var expression in expressionArray)
            {
                if (!first)
                {
                    writer.Write(joiner);
                    if (useNewLine)
                    {
                        writer.WriteLine();
                    }
                }
                writer.Write(expression);
                first = false;
            }
        }

        public static void WriteNewLine(this IndentedTextWriter writer)
        {
            writer.Write("\r\n\t");
        }

        public static void WriteLineJoined(this IndentedTextWriter writer, IEnumerable<string> expressions)
        {
            var expression = string.Join(" , ", expressions);
            writer.WriteLine(expression);
        }

        public static void WriteLineEx(this IndentedTextWriter writer, string expression)
        {
            writer.WriteEx(expression);
            writer.WriteLine();
        }
        public static void WriteEx(this IndentedTextWriter writer, string expression)
        {
            if (string.IsNullOrEmpty(expression)) return;
            for (var i = 0; i < expression.Length - 1; i++)
            {
                if (expression[i] == '\r' && expression[i + 1] == '\n')
                {
                    writer.WriteLine();
                    i++;
                }
                else
                {
                    writer.Write(expression[i]);
                }
            }

            if (expression[expression.Length - 1] != '\n')
                writer.Write(expression[expression.Length - 1]);
        }
        public static void Write2(this IndentedTextWriter writer, string expression = "")
        {
            writer.WriteScoped(expression, SQLKeywords.SPACE, SQLKeywords.SPACE);
            //writer.Write(SPACE);
            //writer.Write(expression);
            //writer.Write(SPACE);
        }
        public static void WriteWithBeginEnd(this IndentedTextWriter writer, string expression)
        {
            writer.WriteLine(SQLKeywords.BEGIN);
            writer.Indent++;
            writer.WriteLine(expression);
            writer.Indent--;
            writer.WriteLine(SQLKeywords.END);
        }

    }
}