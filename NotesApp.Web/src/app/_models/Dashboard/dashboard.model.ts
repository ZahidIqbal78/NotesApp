import { ReminderStats } from "./reminderStats.model";
import { TodoStats } from "./todoStats.model";

export class DashboardModel {
  reminders: ReminderStats[];
  todos: TodoStats[];
}
