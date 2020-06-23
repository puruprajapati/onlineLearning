import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

// Import Containers
import { DefaultLayoutComponent } from "./containers";

import { P404Component } from "./views/application/error/404.component";
import { P500Component } from "./views/application/error/500.component";
import { LoginComponent } from "./views/application/login/login.component";
import { RegisterComponent } from "./views/application/register/register.component";
import { AuthGuard } from "./helpers";

export const routes: Routes = [
  {
    path: "",
    redirectTo: "dashboard",
    pathMatch: "full",
  },
  {
    path: "404",
    component: P404Component,
    data: {
      title: "Page 404",
    },
  },
  {
    path: "500",
    component: P500Component,
    data: {
      title: "Page 500",
    },
  },
  {
    path: "login",
    component: LoginComponent,
    data: {
      title: "Login Page",
    },
  },
  {
    path: "register",
    component: RegisterComponent,
    data: {
      title: "Register Page",
    },
  },
  {
    path: "",
    component: DefaultLayoutComponent,
    data: {
      title: "Home",
    },
    children: [
      {
        path: "dashboard",
        loadChildren: () =>
          import("./views/application/dashboard/dashboard.module").then(
            (m) => m.DashboardModule
          ),
        canActivate: [AuthGuard],
      },
      {
        path: "settings",
        loadChildren: () =>
          import("./views/application/settings/settings.module").then(
            (m) => m.SettingModule
          ),
      },
      {
        path: "session",
        loadChildren: () =>
          import("./views/application/session/session.module").then(
            (m) => m.SessionModule
          ),
      },
    ],
  },
  { path: "**", component: P404Component },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
