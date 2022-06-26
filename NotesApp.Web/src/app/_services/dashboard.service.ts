import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { DashboardModel } from '../_models/Dashboard/dashboard.model';

@Injectable({ providedIn: 'root' })
export class DashboardService {
  constructor(private http: HttpClient) {}

  getTodayDashboardStats() {
    return this.http.get<DashboardModel>(
      `${environment.apiUrl}/api/Dashboard/today`
    );
  }

  getNextSevenDaysDashboardStats() {
    return this.http.get<DashboardModel>(
      `${environment.apiUrl}/api/Dashboard/nextseven`
    );
  }

  getNextMonthDashboardStats() {
    return this.http.get<DashboardModel>(
      `${environment.apiUrl}/api/Dashboard/nextmonth`
    );
  }
}
