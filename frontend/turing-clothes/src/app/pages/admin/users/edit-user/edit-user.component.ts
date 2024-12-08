import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { User } from '../../../../models/user';
import { AdminService } from '../../../../services/admin.service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { SidebarComponent } from "../../../../shared/sidebar/sidebar.component";

@Component({
  selector: 'app-edit-user',
  standalone: true,
  imports: [FormsModule, SidebarComponent, RouterLink],
  templateUrl: './edit-user.component.html',
  styleUrl: './edit-user.component.css',
})
export class EditUserComponent implements OnInit {
  user: User;
  email: string;

  constructor(
    private adminService: AdminService,
    private route: ActivatedRoute
  ) {}

  async ngOnInit(): Promise<void> {
    this.email = this.route.snapshot.queryParamMap.get(
      'userEmail'
    ) as unknown as string;
    const result = await this.adminService.getUserByEmail(this.email);
    if (result.success) {
      this.user = result.data;
    } else {
      console.error(result.error);
    }
  }

  async editUserRole(): Promise<void> {
    await this.adminService.updateUserRole(this.user.email, this.user.role);
  }
}
