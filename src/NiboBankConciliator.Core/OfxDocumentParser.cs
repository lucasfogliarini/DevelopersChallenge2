using NiboBankConciliator.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace NiboBankConciliator.Core
{
    public static class OfxDocumentParser
    {
        public static OfxDocument Parse(Stream stream)
        {
            OfxDocument ofxDocument = new OfxDocument();
            using (StreamReader reader = new StreamReader(stream))
            {
                ofxDocument.Transactions = new List<OfxTransaction>();
                bool inHeader = true;
                while (!reader.EndOfStream)
                {
                    string temp = reader.ReadLine();
                    if (inHeader)
                    {
                        if (temp.ToLower().Contains("<ofx>"))
                        {
                            inHeader = false;
                        }
                        #region Read Header
                        else
                        {
                            string[] tempSplit = temp.Split(":".ToCharArray());
                            switch (tempSplit[0].ToLower())
                            {
                                case "ofxheader":
                                    ofxDocument.OfxHeader = tempSplit[1];
                                    break;
                                case "data":
                                    ofxDocument.Data = tempSplit[1];
                                    break;
                                case "version":
                                    ofxDocument.Version = tempSplit[1];
                                    break;
                                case "security":
                                    ofxDocument.Security = tempSplit[1];
                                    break;
                                case "encoding":
                                    ofxDocument.Encoding = tempSplit[1];
                                    break;
                                case "charset":
                                    ofxDocument.Charset = tempSplit[1];
                                    break;
                                case "compression":
                                    ofxDocument.Compression = tempSplit[1];
                                    break;
                                case "oldfileuid":
                                    ofxDocument.OldFileUID = tempSplit[1];
                                    break;
                                case "newfileuid":
                                    ofxDocument.NewFileUID = tempSplit[1];
                                    break;
                            }
                        }
                        #endregion
                    }
                    if (!inHeader)
                    {
                        string restOfFile = temp + reader.ReadToEnd();
                        restOfFile = Regex.Replace(restOfFile, Environment.NewLine, "");
                        restOfFile = Regex.Replace(restOfFile, "\n", "");
                        restOfFile = Regex.Replace(restOfFile, "\t", "");
                        ofxDocument.BankID = Regex.Match(restOfFile, @"(?<=bankid>).+?(?=<)", RegexOptions.Multiline | RegexOptions.IgnoreCase).Value;
                        ofxDocument.AccountID = Regex.Match(restOfFile, @"(?<=acctid>).+?(?=<)", RegexOptions.Multiline | RegexOptions.IgnoreCase).Value;
                        ofxDocument.AccountType = Regex.Match(restOfFile, @"(?<=accttype>).+?(?=<)", RegexOptions.Multiline | RegexOptions.IgnoreCase).Value;
                        ofxDocument.StartDate = Regex.Match(restOfFile, @"(?<=dtstart>).+?(?=<)", RegexOptions.Multiline | RegexOptions.IgnoreCase).Value;
                        ofxDocument.EndDate = Regex.Match(restOfFile, @"(?<=dtend>).+?(?=<)", RegexOptions.Multiline | RegexOptions.IgnoreCase).Value;
                        string banktranlist = Regex.Match(restOfFile, @"(?<=<banktranlist>).+(?=<\/banktranlist>)", RegexOptions.Multiline | RegexOptions.IgnoreCase).Value;

                        MatchCollection m = Regex.Matches(banktranlist, @"<stmttrn>.+?<\/stmttrn>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                        foreach (Match match in m)
                        {
                            foreach (Capture capture in match.Captures)
                            {
                                OfxTransaction trans = new OfxTransaction();
                                if (Regex.Match(capture.Value, @"(?<=<trntype>).+?(?=<)", RegexOptions.Multiline | RegexOptions.IgnoreCase).Value.ToLower().Equals("credit"))
                                    trans.TransType = TransType.Credit;
                                if (Regex.Match(capture.Value, @"(?<=<trntype>).+?(?=<)", RegexOptions.Multiline | RegexOptions.IgnoreCase).Value.ToLower().Equals("debit"))
                                    trans.TransType = TransType.Debit;

                                var datePosted = Regex.Match(capture.Value, @"(?<=<dtposted>).+?(?=<)", RegexOptions.Multiline | RegexOptions.IgnoreCase).Value;
                                trans.DatePosted = DateTime.ParseExact(datePosted.Substring(0, 8), "yyyyMMdd", null);
                                var transAmount = Regex.Match(capture.Value, @"(?<=<trnamt>).+?(?=<)", RegexOptions.Multiline | RegexOptions.IgnoreCase).Value;
                                trans.TransAmount = decimal.Parse(transAmount);
                                trans.Memo = Regex.Match(capture.Value, @"(?<=<memo>).+?(?=<)", RegexOptions.Multiline | RegexOptions.IgnoreCase).Value;
                                ofxDocument.Transactions.Add(trans);
                            }
                        }
                    }
                }
            }
            return ofxDocument;
        }
    }
}
