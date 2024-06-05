export interface CashFlowSeries {
    period: number;
    cashFlow: number;
    presentValue: number;
}

export interface NPVResponse {
    calculatedNPV: number;
    cashFlowSeries: CashFlowSeries[];
}

export interface NPVRangeResponse {
    rate: number;
    calculatedNPV: number;
    cashFlowSeries: CashFlowSeries[];
}