import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HeroService } from '../../../Services/Hero/hero.service';
import { SuperPowerService, SuperPower } from '../../../Services/SuperPower/super-power.service';

@Component({
  selector: 'app-hero-edit',
  standalone: true,
  templateUrl: './edit-hero.component.html',
  styleUrls: ['./edit-hero.component.scss'],
  imports: [FormsModule, CommonModule],
})
export class HeroEditComponent implements OnInit {
  hero: any = {};
  powers: SuperPower[] = [];

  constructor(
    private heroService: HeroService,
    private superPowerService: SuperPowerService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.params['id'];

    this.heroService.getHeroById(id).subscribe(data => {
      if (data.birthDate) {
        data.birthDate = data.birthDate.split('T')[0];
      }

      this.hero = data;

      if (!this.hero.superPowersId) {
        this.hero.superPowersId = [];
      }
    });

    this.superPowerService.getAll().subscribe(powers => {
      this.powers = powers;
    });
  }

  successMessage: string | null = null;
  errorMessage: string | null = null;

  updateHero(): void {
    const id = this.route.snapshot.params['id'];

    this.heroService.updateHero(id, this.hero).subscribe({
      next: () => {
        this.successMessage = 'Herói atualizado com sucesso!';

        setTimeout(() => {
          this.successMessage = null;
          this.router.navigate(['/']);
        }, 2000);
      },
      error: (error) => {
        if (error.status === 409 && error.error?.message) {
          this.errorMessage = error.error.message;
        } else {
          this.errorMessage = 'Erro ao atualizar o herói.';
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