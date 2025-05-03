import { Component, OnInit, Input } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HeroService } from '../../../Services/Hero/hero.service';

@Component({
  selector: 'app-hero-detail',
  standalone: true,
  templateUrl: './hero-details.component.html',
  styleUrls: ['./hero-details.component.scss'],
  imports: [RouterModule],
})
export class HeroDetailComponent implements OnInit {
  @Input() hero: any = {};  // Recebe o herói como Input do componente pai

  constructor(private heroService: HeroService) { }

  ngOnInit(): void {
    // O herói será fornecido pelo componente pai, então não há necessidade de buscar via API aqui.
  }
}
