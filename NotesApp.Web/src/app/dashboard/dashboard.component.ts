import { Component, OnInit } from '@angular/core';
import { DashboardModel } from '../_models/Dashboard/dashboard.model';
import { Note } from '../_models/Note/note.model';
import { UserLoginResponse } from '../_models/User/userloginresponse.model';
import { DashboardService } from '../_services/dashboard.service';
import { NoteService } from '../_services/note.service';
import { UserService } from '../_services/user.service';

@Component({ templateUrl: 'dashboard.component.html' })
export class DashboardComponent implements OnInit {
  title = 'Dashboard';
  dashboardTodayStats: DashboardModel;
  dashboardNextSevenStats: DashboardModel;
  dashboardNextMonthStats: DashboardModel;
  allNotes: Note[];

  constructor(private dashboardService: DashboardService,
    private noteSercive: NoteService) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData() {
    this.dashboardService
      .getTodayDashboardStats()
      .pipe()
      .subscribe((stats) => {
        this.dashboardTodayStats = stats;
      });
    

    this.dashboardService
      .getNextSevenDaysDashboardStats()
      .pipe()
      .subscribe((statsNextSeven) => {
        this.dashboardNextSevenStats = statsNextSeven;
      });
    

    this.dashboardService
      .getNextMonthDashboardStats()
      .pipe()
      .subscribe((statsNextMonth) => {
        this.dashboardNextMonthStats = statsNextMonth;
      });
    
    this.noteSercive.getAllNotes()
      .pipe()
      .subscribe((allNotes) => {
        this.allNotes = allNotes;
      })
  }
}
