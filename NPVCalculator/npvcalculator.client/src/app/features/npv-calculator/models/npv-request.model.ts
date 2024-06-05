export interface NPVRequest {
    initialInvestments: number;
    interestRate: number;
    cashFlows: number[];
    discountRateRange?: DiscountRateRange;
}

export interface DiscountRateRange{
    lowerDiscountRate: number;
    upperDiscountRate: number;
    discountRateIncrement: number;
}