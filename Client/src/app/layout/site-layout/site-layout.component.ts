import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavComponent } from 'src/app/components/nav/nav.component';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-site-layout',
  standalone: true,
  imports: [CommonModule, NavComponent, RouterModule],
  templateUrl: './site-layout.component.html',
  styleUrls: ['./site-layout.component.css']
})
export class SiteLayoutComponent {

}
