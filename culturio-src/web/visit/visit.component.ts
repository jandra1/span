import { Component, OnInit } from '@angular/core';
import { VisitService } from './services/visit.service';
import {
  debounceTime,
  distinctUntilChanged,
  forkJoin,
  fromEvent,
  map,
  Observable,
  shareReplay,
} from 'rxjs';
import * as mapboxgl from 'mapbox-gl';


@Component({
  selector: 'app-visit',
  templateUrl: './visit.component.html',
  styleUrls: ['./visit.component.scss'],
  
})
export class VisitComponent implements OnInit {


  map: mapboxgl.Map;
  style = 'mapbox://styles/mapbox/streets-v11';
  lat = 45.80;
  lng = 15.98;

  constructor(private visitService: VisitService) { }

  ngOnInit(): void {

    this.map = new mapboxgl.Map({
      accessToken: "pk.eyJ1IjoiamFrb3ZpbmhvIiwiYSI6ImNsN3B6c2NvZDAwbmMzdnNhNjFzb3k0OWMifQ.PLpSSPMdnWYBccaXNjgb7A",
      container: 'map',
      style: this.style,
      zoom: 12.5,
      center: [this.lng, this.lat]
    });

    //const marker = new mapboxgl.Marker()
    //.setLngLat([15.98, 45.80])
    //.addTo(this.map);

    // Add map controls
    this.map.addControl(new mapboxgl.NavigationControl());
  }

}
