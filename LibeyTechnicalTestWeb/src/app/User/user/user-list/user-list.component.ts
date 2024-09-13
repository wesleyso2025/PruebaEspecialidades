import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { LibeyUserService } from 'src/app/core/service/libeyuser/libeyuser.service';
import { LibeyUser } from 'src/app/entities/libeyuser';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnChanges {
  
  @Input() searchTerm: string = '';
  users: LibeyUser[] = [];

  filteredUsers: LibeyUser[] = [];
  selectedUser: LibeyUser | null = null;

  constructor(private userService: LibeyUserService, private router: Router) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['searchTerm']) {
      this.loadUsers();
    }
  }

  loadUsers(): void {
    if (this.searchTerm.trim() === '') {
      this.userService.GetAll().subscribe(
        (data) => {
          this.users = data;
        },
        (error) => {
          console.error('Error fetching all users', error);
        }
      );
    } else {
      this.userService.GetAllByTerm(this.searchTerm).subscribe(
        (data) => {
          this.users = data;
        },
        (error) => {
          console.error('Error fetching users by term', error);
        }
      );
    }
  }

  toggleSelection(user: LibeyUser): void {
    this.selectedUser = user;
  }

  isSelected(user: LibeyUser): boolean {
    return this.selectedUser === user;
  }

  addUser(): void {
    this.router.navigate(['/user/maintenance']);
  }

  editUser(): void {
    if (this.selectedUser) {
      this.router.navigate(['/user/maintenance'], { state: { user: this.selectedUser } });
    } else {
      console.error('No user selected for editing.');
    }
  }

}
