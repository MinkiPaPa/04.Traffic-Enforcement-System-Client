# iTops-Client
iTops client program source code

Code by MinkiPaPa.

# 1. Project Objective
   - 프로젝트 수행 기간 : 2019년 5월 1일 ~ 2019년 10월 30일
   - 프로젝트 참여 인력 : PMO 1 , Dev 2
   - 프로젝트 수행 목표
      - 수동 단속 프로세스의 자동화
      - 자동차 번호판 인식 기능의 적용 ( OpenALPR 적용 )
      - 남아공 eNaTis ( National Traffic Information System )과 데이터 연동
      - 시스템이 적용되는 각 도시별 결제 이력 연동을 위한 ERP 연동
   
# 2. Solution Process Diagram
![image](https://user-images.githubusercontent.com/97417837/149051915-aaedd548-14dd-4596-90d7-bb174409529c.png)

# 3. DataBase Structure
   - 기존의 TBOS ( 구버전 iTops )의 데이터베이스가 MSSQL 기반으로 되어있어 기존의 데이터 호환 관계를 유지하기위하여
      MSSQL을 기본 데이터베이스로 선정
   - 비지니스를 제공하는 남아프리카공화국의 각 시도별 교통경찰서, 시경에 MS Azure 클라우드 서비스를 MS와 계약하여 제공받고 있는 상황으로
      도시별 사용하는 환경과의 원활한 데이터베이스 링크를 하는데 MSSQL이 촉박한 개발의 일정상 가장 좋은 선택지였음
      [Sample_BankPaid DATA_ST20190520.TXT.txt](https://github.com/MinkiPaPa/04.iTops-Client/files/7851194/Sample_BankPaid.DATA_ST20190520.TXT.txt)
   - 선정 비교 대상군의 타 오픈소스기반의 DBMS들과 비교시 별도의 튜닝없이도 좋은 퍼포먼스를 보였었고, Hot Backup 기능 및 시 ICT 담당자가
      유지보수에 참여하는 경우 선호하는 시스템이었음
   - iTops 데이터베이스 설계 설명
     - iTops Client와 Server는 각각 별도의 세팅 쿼리 파일을 분리해 관리
       - [iTOPS Client DB Initial_202002_BryanKim.txt](https://github.com/MinkiPaPa/04.iTops-Client/files/7851235/iTOPS.Client.DB.Initial_202002_BryanKim.txt)
     - iTops Server에 적용되는 데이터베이스는 신규로 DB 구조, 테이블 설계 진행함
       - [iTOPS Server DB Initial_202002_BryanKim.txt](https://github.com/MinkiPaPa/04.iTops-Client/files/7851234/iTOPS.Server.DB.Initial_202002_BryanKim.txt)
     - iTops Server에 적용되는 데이터베이스는 기존의 TBOS와 원활한 연동을 위하여 기존 설계된 백오피스의 데이터베이스 구조를 대부분 승계하며 신규 추가 기능에 맞춰 DB 보완 설계 진행함
       - [iTOPS Location Code_Newcastle_202002_BryanKim.txt](https://github.com/MinkiPaPa/04.iTops-Client/files/7851236/iTOPS.Location.Code_Newcastle_202002_BryanKim.txt)

     ![image](https://user-images.githubusercontent.com/97417837/149053582-7aa8e4ab-242c-4497-9555-97105cf8d33a.png)
     ![image](https://user-images.githubusercontent.com/97417837/149053601-17580737-6f65-4eb6-950a-6dec6f89e0ac.png)

# 4. Develop Labguage
   - TBOS ( 구 iTops )에서는 VS6.0 C++과 WTL 기반의 GUI 프로그래밍으로 작업이 되어있었음
   - iTops 개발을 기획하고 진행하면서 언어 및 외부 라이브러리에 대한 선정은 아래와 같이 진행하였음
     - 개발 언어 : C#
       - 선정 이유 : 기존의 TBOS의 일부 C++ 언어 구문을 호환하며 승계 및 차후 남아공 현지에서 C++ 개발자보다 C# 개발자의 수급이 원활할 것으로 예측하여 개발 언어로 C#을 선정함
       - 이후 계획 : C# 첫 프로젝트였기 때문에 기존의 문법적 표현이 C++에 많이 가깝게 개발이 되었으며 현지의 상황이 기획 시기때 예측과는 다르데 C++, C# 개발자 모두 적정 개발 능력을 보유한 개발자의 수급이 원활하지 않을 것으로 예상되어 이 후 차기버전은 보다 개발리소스 확보에 유리한 개발언어로 개발해야할 수 있다는 부담감이 있음
     - 외부 프레임워크, 라이브러리 : Infragistics , LTI Trucam II SDK , OpenALPR
       - 선정 이유 : 참여 개발자들이 Infragistics 활용한 개발의 경험이 있었고 OpenALPR 라이브러리는 데이터 Import PC가 지정된 PC였기 때문에 On-Ppromise $600의 저렴한 비용으로 활용가능한 남아프리카공화국 번호판 데이터를 포함한 상용 라이브러리여서 선정함.
       - 이후 계획 : 이후 개발자들이 선호하는 외부 라이브러리로 변경 적용 가능성 열어둠 ( 예 : QT )
