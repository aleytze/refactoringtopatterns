﻿﻿using NUnit.Framework;
using System;
using ReplaceConstructorsWithCreationMethods.MyWork;

namespace RefactoringToPatterns.ReplaceConstructorsWithCreationMethods.MyWork
{
    [TestFixture]
    public class CapitalCalculationTests
    {
        private readonly DateTime? _maturity = new DateTime(2020, 10, 2);
        private readonly DateTime? _expiry = new DateTime(2021, 10, 2);

        private const double Commitment = 3.0;
        private const double Outstanding = 12.9;
        private RiskAdjustedCapitalStrategy _riskAdjustedCapitalStrategy = 
            new RiskAdjustedCapitalStrategy();
        const int RiskRating = 5;

        [SetUp]
        public void Init()
        {
        }

        [Test]
        public void test_term_loan_no_payments()
        {
            Loan loan = Loan.CreateTermLoan(Commitment, RiskRating, _maturity);
            Assert.IsInstanceOf(typeof(CapitalStrategyTermLoan), loan.CapitalStrategy);
        }

        [Test]
        public void test_term_loan_one_payment()
        {
            Loan loan = Loan.CreateTermLoan(Commitment, RiskRating, _maturity);
            Assert.IsInstanceOf(typeof(CapitalStrategyTermLoan), loan.CapitalStrategy);
        }

        [Test]
        public void test_revolver_loan_no_payments()
        {
            Loan loan = CreateRevolver(Commitment, RiskRating, null, _expiry);
            Assert.IsInstanceOf(typeof(CapitalStrategyRevolver), loan.CapitalStrategy);
        }

        public static Loan CreateRevolver(double commitment, int riskRating, DateTime? maturity, DateTime? expiry)
        {
            return new Loan(commitment, riskRating, maturity, expiry);
        }

        [Test]
        public void test_RCTL_loan_one_payment()
        {
            Loan loan = new Loan(Commitment, Outstanding, RiskRating, _maturity, _expiry);
            Assert.IsInstanceOf(typeof(CapitalStrategyRCTL), loan.CapitalStrategy);
        }

        [Test]
        public void test_term_loan_with_risk_adjusted_capital_strategy()
        {
			Loan loan = Loan.CreateTermLoan(_riskAdjustedCapitalStrategy,
                                       Commitment, Outstanding, RiskRating,
                                       _maturity, null);

            Assert.IsInstanceOf(typeof(RiskAdjustedCapitalStrategy), loan.CapitalStrategy);
        }
    }
}