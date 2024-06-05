import { TestBed } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController,
} from '@angular/common/http/testing';
import { NpvService } from './npv.service';
import { NPVRequest } from '../models/npv-request.model';
import { NPVRangeResponse, NPVResponse } from '../models/npv-response.model';

describe('NpvService', () => {
  let service: NpvService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [NpvService],
    });

    service = TestBed.inject(NpvService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should calculate NPV with cash flow stream', () => {
    const npvRequest: NPVRequest = {
      initialInvestments: 100000,
      interestRate: 1,
      cashFlows: [10000, 20000, 30000],
    };

    const mockResponse: NPVResponse = {
      calculatedNPV: 50000,
      cashFlowSeries: [
        { period: 0, cashFlow: -100000, presentValue: -100000 },
        { period: 1, cashFlow: 10000, presentValue: 9090.91 },
        { period: 2, cashFlow: 20000, presentValue: 16528.93 },
      ],
    };

    service.calculateNPVWithCashFlowStream(npvRequest).subscribe((response) => {
      expect(response).toEqual(mockResponse);
    });

    const req = httpTestingController.expectOne('/api/calculator/npv');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(npvRequest);
    req.flush(mockResponse);
  });

  it('should calculate NPV range with cash flow stream', () => {
    const npvRequest: NPVRequest = {
      initialInvestments: 100000,
      interestRate: 1,
      cashFlows: [10000, 20000, 30000],
      discountRateRange: {
        lowerDiscountRate: 5,
        upperDiscountRate: 15,
        discountRateIncrement: 1,
      },
    };

    const mockResponse: NPVRangeResponse[] = [
      {
        rate: 0.1,
        calculatedNPV: 45000,
        cashFlowSeries: [
          { period: 0, cashFlow: -100000, presentValue: -100000 },
          { period: 1, cashFlow: 10000, presentValue: 9090.91 },
          { period: 2, cashFlow: 20000, presentValue: 16528.93 },
        ],
      },
    ];

    service
      .calculateNPVRangeWithCashFlowStream(npvRequest)
      .subscribe((response) => {
        expect(response).toEqual(mockResponse);
      });

    const req = httpTestingController.expectOne('api/calculator/npv-range');
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(npvRequest);
    req.flush(mockResponse);
  });
});
