import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NpvCashFlowResultComponent } from './npv-cash-flow-result.component';
import { NPVResponse, NPVRangeResponse } from '../models/npv-response.model';
import { provideMockStore } from '@ngrx/store/testing';
import { NpvService } from '../services/npv.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { HttpClient } from '@angular/common/http';

describe('NpvCashFlowResultComponent', () => {
  let component: NpvCashFlowResultComponent;
  let fixture: ComponentFixture<NpvCashFlowResultComponent>;

  const initialState = {
    npv: {
      npvWithCashFlowResult: null,
      npvRangeResult: null,
    },
  };

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [NpvCashFlowResultComponent],
      providers: [provideMockStore({ initialState }), NpvService, HttpClient],
      imports: [HttpClientTestingModule],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NpvCashFlowResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display NPV With Cash Flow Result when data is provided', () => {
    const mockData: NPVResponse = {
      calculatedNPV: 100000,
      cashFlowSeries: [
        { period: 0, cashFlow: -100000, presentValue: -100000 },
        { period: 1, cashFlow: 10000, presentValue: 9000 },
        { period: 2, cashFlow: 20000, presentValue: 18000 },
      ],
    };

    component.npvWithCashFlowResult = mockData;
    fixture.detectChanges();

    const calculatedNPVElement =
      fixture.debugElement.nativeElement.querySelector('h3');
    expect(calculatedNPVElement?.textContent).toContain(
      'Calculated NPV: 100000'
    );

    const rows =
      fixture.debugElement.nativeElement.querySelectorAll('tbody tr');
    expect(rows.length).toBe(3);
    expect(rows[0].textContent).toContain('0');
    expect(rows[0].textContent).toContain('-100000');
    expect(rows[0].textContent).toContain('-100000');
  });

  it('should display NPV Range with Cash Flow Stream when data is provided', () => {
    const mockData: NPVRangeResponse[] = [
      {
        rate: 0.1,
        calculatedNPV: 95000,
        cashFlowSeries: [
          { period: 0, cashFlow: -100000, presentValue: -100000 },
          { period: 1, cashFlow: 10000, presentValue: 9090.91 },
          { period: 2, cashFlow: 20000, presentValue: 16528.93 },
        ],
      },
    ];

    component.npvRangeResult = mockData;
    fixture.detectChanges();

    const rateElement = fixture.debugElement.nativeElement.querySelector('h4');
    expect(rateElement?.textContent).toContain('Rate: 0.1');

    const rows =
      fixture.debugElement.nativeElement.querySelectorAll('tbody tr');
    expect(rows.length).toBe(3);
    expect(rows[0].textContent).toContain('0');
    expect(rows[0].textContent).toContain('-100000');
    expect(rows[0].textContent).toContain('-100000');
  });
});
