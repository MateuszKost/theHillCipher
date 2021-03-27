.code

Multi3 proc

;zerowanie rejestru
xor rax, rax

movq mm0,  [rcx]  ;przypisanie do rejestru mm0 1 elementu 1 tablicy
movq mm1,  [rdx]  ;przypisanie do rejestru mm1 1 elementu 2 tablicy

pmaddwd MM0, MM1 ; dodanie tych elementów

;reszta analogicznie

movq mm2,  [rcx+4] 
movq mm3,  [rdx+4]

pmaddwd MM2, MM3

movq mm4,  [rcx+8]
movq mm5,  [rdx+8]

pmaddwd MM4, MM5

paddd MM2, MM4

paddd MM0, MM2

;przypisanie wyniku do eax, aby otrzymaæ poprawny wynik
movd eax, mm0

ret

Multi3 endp

Multi2 proc

;zerowanie rejestru
xor rax, rax

movq mm0,  [rcx]  ;przypisanie do rejestru mm0 1 elementu 1 tablicy
movq mm1,  [rdx]  ;przypisanie do rejestru mm1 1 elementu 2 tablicy

pmaddwd MM0, MM1  ; dodanie tych elementów

;reszta analogicznie
movq mm2,  [rcx+4]
movq mm3,  [rdx+4]

pmaddwd MM2, MM3

paddd MM0, MM2

;przypisanie wyniku do eax, aby otrzymaæ poprawny wynik
movd eax, mm0

ret

Multi2 endp

end