# iTops-Client
iTops client program source code

Code by MinkiPaPa.

1. Project Objective
   - 수동 단속 프로세스의 자동화
   - 자동차 번호판 인식 기능의 적용 ( OpenALPR 적용 )
   - 남아공 eNaTis ( National Traffic Information System )과 데이터 연동
   - 시스템이 적용되는 각 도시별 결제 이력 연동을 위한 ERP 연동
   
2. Solution Process Diagram
![image](https://user-images.githubusercontent.com/97417837/149051915-aaedd548-14dd-4596-90d7-bb174409529c.png)

3. DataBase Structure
   - 기존의 TBOS ( 구버전 iTops )의 데이터베이스가 MSSQL 기반으로 되어있어 기존의 데이터 호환 관계를 유지하기위하여
      MSSQL을 기본 데이터베이스로 선정
   - 비지니스를 제공하는 남아프리카공화국의 각 시도별 교통경찰서, 시경에 MS Azure 클라우드 서비스를 MS와 계약하여 제공받고 있는 상황으로
      도시별 사용하는 환경과의 원활한 데이터베이스 링크를 하는데 MSSQL이 촉박한 개발의 일정상 가장 좋은 선택지였음
      [Sample_BankPaid DATA_ST20190520.TXT.txt](https://github.com/MinkiPaPa/04.iTops-Client/files/7851194/Sample_BankPaid.DATA_ST20190520.TXT.txt)
   - 선정 비교 대상군의 타 오픈소스기반의 DBMS들과 비교시 별도의 튜닝없이도 좋은 퍼포먼스를 보였었고, Hot Backup 기능 및 시 ICT 담당자가
      유지보수에 참여하는 경우 선호하는 시스템이었음


