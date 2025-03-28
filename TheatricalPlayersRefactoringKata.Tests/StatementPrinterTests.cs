using ApprovalTests;
using ApprovalTests.Reporters;
using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services.StatementPrinter;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
        plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
        plays.Add("othello", new Play("Othello", 3560, "tragedy"));
        plays.Add("henry-v", new Play("Henry V", 3227, "history"));
        plays.Add("john", new Play("King John", 2648, "history"));
        plays.Add("richard-iii", new Play("Richard III", 3718, "history"));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
            new Performance("hamlet", 55),
            new Performance("as-like", 35),
            new Performance("othello", 40),
            new Performance("henry-v", 20),
            new Performance("john", 39),
            new Performance("richard-iii", 20)
            }
        );

        StatementPrinterService statementPrinter = new StatementPrinterService();
        var result = statementPrinter.Print(invoice, plays).Statement;

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestPlayLinesNumber()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
        plays.Add("as-like", new Play("As You Like It", 1200, "comedy"));
        plays.Add("othello", new Play("Othello", 5478, "tragedy"));
        plays.Add("henry-v", new Play("Henry V", 2000, "history"));
        plays.Add("john", new Play("King John", 2648, "history"));
        plays.Add("richard-iii", new Play("Richard III", 980, "history"));

        StatementPrinterService statementPrinter = new StatementPrinterService();
        var result = string.Format("Number of lines:\n");

        foreach (var play in plays.Values)
        {
            result += string.Format("Play: {0}\n", play.Name);
            result += string.Format("Lines: {0}\n", statementPrinter.ValidatePlayLines(play.Lines));
        }
       
        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestAmountByPlayTypeTragedy()
    {
        var result = string.Empty;
        StatementPrinterService statementPrinter = new StatementPrinterService();
        result += string.Format("amount: {0}\n", Convert.ToDecimal(statementPrinter.GetAmountByPlayTypeTragedy(35, 40000) /100));
        result += string.Format("amount: {0}\n", Convert.ToDecimal(statementPrinter.GetAmountByPlayTypeTragedy(20, 24000) / 100));
        result += string.Format("amount: {0}\n", Convert.ToDecimal(statementPrinter.GetAmountByPlayTypeTragedy(45, 34500) / 100));
        result += string.Format("amount: {0}\n", Convert.ToDecimal(statementPrinter.GetAmountByPlayTypeTragedy(23, 27680) / 100));

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestAmountByPlayTypeComedy()
    {
        var result = string.Empty;
        StatementPrinterService statementPrinter = new StatementPrinterService();
        result += string.Format("amount: {0}\n", Convert.ToDecimal(statementPrinter.GetAmountByPlayTypeComedy(35, 43560) / 100));
        result += string.Format("amount: {0}\n", Convert.ToDecimal(statementPrinter.GetAmountByPlayTypeComedy(20, 25670) / 100));
        result += string.Format("amount: {0}\n", Convert.ToDecimal(statementPrinter.GetAmountByPlayTypeComedy(45, 34500) / 100));
        result += string.Format("amount: {0}\n", Convert.ToDecimal(statementPrinter.GetAmountByPlayTypeComedy(23, 12300) / 100));

        Approvals.Verify(result);
    }


    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestAmountByPlayTypeHistory()
    {
        var result = string.Empty;
        StatementPrinterService statementPrinter = new StatementPrinterService();
        result += string.Format("amount: {0}\n", Convert.ToDecimal(statementPrinter.GetAmountByPlayTypeHistory(35, 39800) / 100));
        result += string.Format("amount: {0}\n", Convert.ToDecimal(statementPrinter.GetAmountByPlayTypeHistory(20, 24000) / 100));
        result += string.Format("amount: {0}\n", Convert.ToDecimal(statementPrinter.GetAmountByPlayTypeHistory(45, 35780) / 100));
        result += string.Format("amount: {0}\n", Convert.ToDecimal(statementPrinter.GetAmountByPlayTypeHistory(23, 19460) / 100));

        Approvals.Verify(result);
    }


    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatement()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
        plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
        plays.Add("othello", new Play("Othello", 3560, "tragedy"));
        plays.Add("henry-v", new Play("Henry V", 3227, "history"));
        plays.Add("john", new Play("King John", 2648, "history"));
        plays.Add("richard-iii", new Play("Richard III", 3718, "history"));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
            new Performance("hamlet", 55),
            new Performance("as-like", 35),
            new Performance("othello", 40),
            new Performance("henry-v", 20),
            new Performance("john", 39),
            new Performance("richard-iii", 20)
            }
        );

        StatementPrinterService statementPrinter = new StatementPrinterService();
        var result = statementPrinter.Print(invoice, plays).XmlStatement;

        Approvals.Verify(result);
    }

}
