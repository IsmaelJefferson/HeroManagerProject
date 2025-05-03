import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeroService } from '../../../Services/Hero/hero.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-hero-list',
  standalone: true,
  templateUrl: './list-heroes.component.html',
  styleUrls: ['./list-heroes.component.scss'],
  imports: [CommonModule, RouterModule]
})

export class HeroListComponent implements OnInit {
  heroes: any[] = [];
  selectedHero: any = null;
  showModal = false;

  constructor(private heroService: HeroService) { }

  ngOnInit(): void {
    this.heroService.getHeroes().subscribe(data => {
      this.heroes = data;
    });
  }

  successMessage: string | null = null;

  deleteHero(id: number): void {
    this.heroService.deleteHero(id).subscribe(() => {
      this.heroes = this.heroes.filter(hero => hero.id !== id);
      this.successMessage = 'Herói deletado com sucesso';

      setTimeout(() => {
        this.successMessage = null;
      }, 3000);
    });
  }

  openDetails(id: number): void {
    this.heroService.getHeroById(id).subscribe(data => {
      this.selectedHero = data;
      this.showModal = true;
    });
  }

  closeModal(): void {
    this.showModal = false;
    this.selectedHero = null;
  }
}