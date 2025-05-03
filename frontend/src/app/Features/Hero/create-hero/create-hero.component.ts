import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { HeroService } from '../../../Services/Hero/hero.service';
import { SuperPowerService, SuperPower } from '../../../Services/SuperPower/super-power.service';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';


@Component({
  selector: 'app-hero-add',
  standalone: true,
  templateUrl: './create-hero.component.html',
  styleUrls: ['./create-hero.component.scss'],
  imports: [FormsModule, CommonModule, HttpClientModule],
})

export class HeroAddComponent implements OnInit {
  hero: any = {
    name: '',
    heroName: '',
    birthDate: '',
    height: 0,
    weight: 0,
    superPowersId: []
  };

  powers: SuperPower[] = [];
  successMessage: string | null = null;

  constructor(
    private heroService: HeroService,
    private powerService: SuperPowerService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.powerService.getAll().subscribe({
      next: powers => this.powers = powers,
      error: err => console.error('Erro ao carregar poderes:', err)
    });
  }

  errorMessage: string | null = null;

  addHero(): void {
    this.heroService.addHero(this.hero).subscribe({
      next: () => {
        this.successMessage = 'Herói cadastrado com sucesso!';

        setTimeout(() => {
          this.successMessage = null;
          this.router.navigate(['/']);
        }, 2000);
      },
      error: (error) => {
        if (error.status === 409 && error.error?.message) {
          this.errorMessage = error.error.message;
        } else {
          this.errorMessage = 'Ocorreu um erro inesperado ao adicionar o herói.';
        }

        setTimeout(() => {
          this.errorMessage = null;
        }, 4000);
      }
    });
  }

  showPowers = false;

  togglePowers(): void {
    this.showPowers = !this.showPowers;
  }

  onPowerToggle(event: any): void {
    const powerId = event.target.value;
    const isChecked = event.target.checked;

    if (isChecked) {
      this.hero.superPowersId.push(powerId);
    } else {
      this.hero.superPowersId = this.hero.superPowersId.filter((id: string) => id !== powerId);
    }
  }
}
