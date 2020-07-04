import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { AuthenticationService } from "../../services";
import { User } from "../../models";
import { navItems } from "../../_nav";
import { EnumRole } from "../../enums";

@Component({
  selector: "app-dashboard",
  templateUrl: "./default-layout.component.html",
})
export class DefaultLayoutComponent {
  currentUser: User;

  public sidebarMinimized = false;
  public navItems = navItems;

  constructor(
    private authenticationService: AuthenticationService,
    private router: Router
  ) {
    this.authenticationService.currentUser.subscribe(
      (x) => (this.currentUser = x)
    );

    const superAdminMenuList = ["Dashboard", "Users", "School"];
    const adminMenuList = [
      "Dashboard",
      "Session",
      "Assignment",
      "Users",
      "Class",
      "Section",
    ];
    const techerMenuList = ["Dashboard", "Session", "Assignment"];
    const studentMenuList = ["Dashboard", "Assignment"];
    // menu handle as per role, todo: this should be fetched from api
    for (var i = navItems.length; i--; ) {
      if (typeof navItems[i].hasOwnProperty("name")) {
        if (this.currentUser.userRole === EnumRole.SuperAdmin.toString()) {
          if (!superAdminMenuList.includes(navItems[i].name)) {
            navItems.splice(i, 1);
          }
        } else if (this.currentUser.userRole === EnumRole.Admin.toString()) {
          if (!adminMenuList.includes(navItems[i].name)) {
            navItems.splice(i, 1);
          }
        } else if (this.currentUser.userRole === EnumRole.Teacher.toString()) {
          if (!techerMenuList.includes(navItems[i].name)) {
            navItems.splice(i, 1);
          }
        } else if (
          this.currentUser.userRole === EnumRole.Student.toString() ||
          this.currentUser.userRole === EnumRole.Parent.toString()
        ) {
          if (!studentMenuList.includes(navItems[i].name)) {
            navItems.splice(i, 1);
          }
        }
      }
    }
  }

  toggleMinimize(e) {
    this.sidebarMinimized = e;
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(["/login"]);
  }
}
