﻿using System;

namespace ReplaceConstructorsWithCreationMethods.MyWork
{
	public class Loan
	{
		private double _commitment;
		private readonly double _outstanding;
		private readonly int _riskRating;
		private readonly DateTime? _maturity;
		private readonly DateTime? _expiry;
		private readonly CapitalStrategy _capitalStrategy;

		public Loan(CapitalStrategy capitalStrategy, double commitment,
					double outstanding, int riskRating,
                    DateTime? maturity, DateTime? expiry)
		{
			this._commitment = commitment;
			this._outstanding = outstanding;
			this._riskRating = riskRating;
			this._maturity = maturity;
			this._expiry = expiry;
			this._capitalStrategy = capitalStrategy;

			if (capitalStrategy == null)
			{
				if (expiry == null)
					this._capitalStrategy = new CapitalStrategyTermLoan();
				else if (maturity == null)
					this._capitalStrategy = new CapitalStrategyRevolver();
				else
					this._capitalStrategy = new CapitalStrategyRCTL();
			}
		}

		public CapitalStrategy CapitalStrategy
		{
			get
			{
				return _capitalStrategy;
			}
		}

		public static Loan CreateTermLoan(double commitment, int riskRating, DateTime? maturity)
		{
            return new Loan(null, commitment, 0.00, riskRating, maturity, null);
		}

		public static Loan CreateTermLoan(CapitalStrategy capitalStrategy, double commitment,
								  double outstanding, int riskRating,
								  DateTime? maturity, DateTime? expiry)
		{
			return new Loan(capitalStrategy,
							commitment, outstanding, riskRating,
							maturity, null);
		}

		public static Loan CreateRevolver(double commitment, int riskRating, DateTime? maturity, DateTime? expiry)
		{
			return new Loan(null, commitment, 0.00, riskRating, maturity, expiry);
		}

		public static Loan CreateRevolver(CapitalStrategy capitalStrategy, double commitment, int riskRating, DateTime? maturity, DateTime? expiry)
		{
			return new Loan(capitalStrategy, commitment, 0.00, riskRating, maturity, expiry);
		}

		public static Loan CreateRCTL(double commitment, double outstanding,
				int riskRating, DateTime? maturity, DateTime? expiry)
		{
			return new Loan(null, commitment, outstanding, riskRating, maturity, expiry);
		}

		public static Loan CreateRCTL(CapitalStrategy capitalStrategy, double commitment, double outstanding,
		int riskRating, DateTime? maturity, DateTime? expiry)
		{
			return new Loan(capitalStrategy, commitment, outstanding, riskRating, maturity, expiry);
		}
	}
}